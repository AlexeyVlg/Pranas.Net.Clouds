using Net.Pranas.Client.Common.Business;
using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using Net.Pranas.Client.GoogleDrive.Business.Service;
using Net.Pranas.Client.GoogleDrive.Resources;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request to upload a local file to Google Drive.
    /// </summary>
    public class DriveFileUploadRequest : IDriveRequest<DriveFileInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a request to upload a local file to Google Drive.
        /// </summary>
        /// <param name="localStream">The stream of local file.</param>
        /// <param name="fileInfo">The file information.</param>
        public DriveFileUploadRequest(Stream localStream, DriveFileShortInfo fileInfo)
        {
            if (localStream == null)
            {
                throw new ArgumentNullException("localStream");
            }

            if (fileInfo == null)
            {
                throw new ArgumentNullException("fileInfo");
            }

            UploadContentType = DefaultUploadContentType;
            UploadType = DriveUploadType.Resumable;
            LocalStream = localStream;
            FileInfo = fileInfo;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets the type of the upload request.
        /// </summary>
        [RestParameter("uploadType", ParameterType = ParameterType.QueryString)]
        public DriveUploadType UploadType { get; private set; }

        /// <summary>
        /// Gets or sets the whether to convert this file to the corresponding Google Docs format.
        /// </summary>
        [RestParameter("convert", ParameterType = ParameterType.GetOrPost)]
        public bool? Convert { get; set; }

        /// <summary>
        /// Gets or sets the whether to attempt OCR on .jpg, .png, .gif, or .pdf uploads.
        /// </summary>
        [RestParameter("ocr", ParameterType = ParameterType.GetOrPost)]
        public bool? Ocr { get; set; }

        /// <summary>
        /// If ocr is true, hints at the language to use. Valid values are ISO 639-1 codes.
        /// </summary>
        [RestParameter("ocrLanguage", ParameterType = ParameterType.GetOrPost)]
        public string OcrLanguage { get; set; }

        /// <summary>
        /// Gets or sets the whether to pin the head revision of the uploaded file.
        /// </summary>
        [RestParameter("pinned", ParameterType = ParameterType.GetOrPost)]
        public bool? Pinned { get; set; }

        /// <summary>
        /// Gets or sets the language of the timed text.
        /// </summary>
        [RestParameter("timedTextLanguage", ParameterType = ParameterType.GetOrPost)]
        public string TimedTextLanguage { get; set; }

        /// <summary>
        /// Gets or sets the timed text track name.
        /// </summary>
        [RestParameter("timedTextTrackName", ParameterType = ParameterType.GetOrPost)]
        public string TimedTextTrackName { get; set; }

        /// <summary>
        /// Gets or sets the whether to use the content as indexable text.
        /// </summary>
        [RestParameter("useContentAsIndexableText", ParameterType = ParameterType.GetOrPost)]
        public bool? UseContentAsIndexableText { get; set; }

        /// <summary>
        /// Gets or sets the the visibility of the new file. This parameter is only relevant when convert=false. 
        /// </summary>
        [RestParameter("visibility", ParameterType = ParameterType.GetOrPost)]
        public DriveFileVisibility? Visibility { get; set; }

        /// <summary>
        /// Gets the file information.
        /// </summary>
        [RestBody]
        public DriveFileShortInfo FileInfo { get; private set; }

        /// <summary>
        /// Gets or sets the MIME type of the upload data to be transferred in subsequent requests.
        /// </summary>
        public string UploadContentType { get; set; }

        /// <summary>
        /// The default MIME type of the upload data to be transferred in subsequent requests.
        /// </summary>
        public const string DefaultUploadContentType = "*/*";

        /// <summary>
        /// Gets the local file stream.
        /// </summary>
        public Stream LocalStream { get; private set; }

        /// <summary>
        /// Gets or sets the chunk size.
        /// </summary>
        public uint? ChunkSize { get; set; }

        /// <summary>
        /// The default chunk size;
        /// </summary>
        public const int DefaultChunkSize = 256*1024*16;

        /// <summary>
        /// Gets or sets the timeout in milliseconds to write a file chunk.
        /// </summary>
        public int? WriteChunkTimeout { get; set; }

        /// <summary>
        /// The default timeout in milliseconds to write a file chunk.
        /// </summary>
        public const int DefaultWriteChunkTimeout = 1000*60*20;

        #endregion

        #region Events of File Transferring

        /// <summary>
        /// Occurs on uploading data progress changed.
        /// </summary>
        public event EventHandler<DataTransferringEventArgs> UploadProgress;

        /// <summary>
        /// Raises handlers of UploadProgress event.
        /// </summary>
        /// <param name="position">The current position.</param>
        /// <param name="length">The length of the total data.</param>
        private void RaiseUploadProgress(long position, long length)
        {
            var handler = UploadProgress;

            if (handler != null)
            {
                handler(this, new DataTransferringEventArgs(position, length));
            }
        }

        #endregion

        #region Execution

        public DriveResponse<DriveFileInfo> Execute(DriveClient driveClient)
        {
            Log.Debug("Execute: Uploading a file.");

            if (driveClient == null)
            {
                throw new ArgumentNullException("driveClient");
            }

            uint chunkSize = ChunkSize ?? DefaultChunkSize;
            int writeChunkTimeout = WriteChunkTimeout ?? DefaultWriteChunkTimeout;
            DriveResponse<DriveFileInfo> result = Upload(this, driveClient, chunkSize, writeChunkTimeout);
            return result;
        }

        /// <summary>
        /// Uploads a file with a specified chunk size.
        /// </summary>
        /// <param name="request">The uploading file request.</param>
        /// <param name="driveClient">The Google Drive client.</param>
        /// <param name="chunkSize">The file chunk size.</param>
        /// <param name="writeChunkTimeout">The timeout in milliseconds to write a file chunk.</param>
        /// <returns>A response with information about remote file.</returns>
        private static DriveResponse<DriveFileInfo> Upload(DriveFileUploadRequest request, DriveClient driveClient, uint chunkSize, int writeChunkTimeout)
        {
            const string locationHeaderName = "Location";
            var log = Log;
            log.Debug("Upload: Chunk size: \"{0}\"", chunkSize);
            var stream = request.LocalStream;
            var initRestRequest = CreateInitRestRequest(request);
            var driveRestClient = driveClient.DriveRestClient;
            var initRestResponse = RequestHandler.Request(driveRestClient, initRestRequest, HttpStatusCode.OK);
            var locationHeader = initRestResponse.Headers.FirstOrDefault(x => x.Name == locationHeaderName);
            IRestResponse lastrestResponse = initRestResponse;

            if (locationHeader != null)
            {
                string uploadContentType = request.UploadContentType;
                var location = locationHeader.Value as string;
                Debug.Assert(location != null, "location != null");
                var uploadClient = new RestClient(location) {Authenticator = driveClient.Authenticator.RestAuthenticator};
                var buffer = new byte[chunkSize];
                long filePos = 0,
                    fileLen = stream.Length;
                int bufferLen;

                request.RaiseUploadProgress(filePos, fileLen);

                while ((bufferLen = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    const string rangeHeaderName = "Range";
                    log.Trace("Upload: Uploading a chunk of file. FilePos: \"{0}\"; BufferLen: \"{1}\"; FileLen: \"{2}\"", filePos, bufferLen, fileLen);
                    var data = bufferLen < buffer.Length ? CreateArray(buffer, 0, bufferLen) : buffer;
                    IRestRequest uploadRestRequest = CreateUploadRestRequest(uploadContentType, chunkSize, filePos, bufferLen, fileLen, data);
                    uploadRestRequest.Timeout = writeChunkTimeout;
                    lastrestResponse = RequestHandler.Request(uploadClient, uploadRestRequest, ResumeIncomplete, HttpStatusCode.Created, HttpStatusCode.OK);

                    if (lastrestResponse.StatusCode == HttpStatusCode.Created || lastrestResponse.StatusCode == HttpStatusCode.OK)
                    {
                        request.RaiseUploadProgress(filePos + bufferLen, fileLen);
                        break;
                    }

                    var rangeResultHeader = lastrestResponse.Headers.FirstOrDefault(x => x.Name == rangeHeaderName);
                    if (rangeResultHeader != null)
                    {
                        var rangeResultStr = rangeResultHeader.Value as string;

                        if (rangeResultStr == null)
                        {
                            throw new InteractionException(MessageDefs.ClientName, MessageDefs.InvalidRangeHeaderOfUploadResponse,
                                lastrestResponse.StatusCode, lastrestResponse.StatusDescription,
                                string.Format(LocalStrings.RangeHeaderInvalidErrorMessage1, rangeResultHeader.Value));
                        }

                        string[] rangeResultItems = rangeResultStr.Replace(" ", string.Empty).Split('-');

                        if (rangeResultItems.Length > 1)
                        {
                            long endRange;

                            if (long.TryParse(rangeResultItems[1], out endRange))
                            {
                                filePos = endRange == 0 ? 0 : endRange + 1;

                                if (filePos != stream.Position)
                                {
                                    stream.Seek(filePos, SeekOrigin.Begin);
                                }

                                request.RaiseUploadProgress(filePos, fileLen);
                            }
                            else
                            {
                                throw new InteractionException(MessageDefs.ClientName, MessageDefs.InvalidRangeHeaderOfUploadResponse,
                                    lastrestResponse.StatusCode, lastrestResponse.StatusDescription,
                                    string.Format(LocalStrings.RangeHeaderInvalidEndErrorMessage1, rangeResultStr));
                            }
                        }
                        else
                        {
                            throw new InteractionException(MessageDefs.ClientName, MessageDefs.InvalidRangeHeaderOfUploadResponse,
                                lastrestResponse.StatusCode, lastrestResponse.StatusDescription,
                                string.Format(LocalStrings.RangeHeaderInvalidEndErrorMessage1, rangeResultStr));
                        }
                    }
                    else
                    {
                        throw new InteractionException(MessageDefs.ClientName, MessageDefs.NoRangeHeaderOfUploadResponse,
                            lastrestResponse.StatusCode, lastrestResponse.StatusDescription, LocalStrings.RangeHeaderNotExistErrorMessage);
                    }
                }
            }
            else
            {
                throw new InteractionException(MessageDefs.ClientName, MessageDefs.NoLocationHeaderOfUploadResponse,
                    lastrestResponse.StatusCode, lastrestResponse.StatusDescription, LocalStrings.LocationHeaderNotExistsMessage);
            }

            var result = new DriveResponse<DriveFileInfo>(lastrestResponse);
            return result;
        }

        /// <summary>
        /// Creates initialization REST request.
        /// </summary>
        /// <param name="request">The instance of uploading file request.</param>
        /// <returns>A REST request to initialize uploading a file with chunks.</returns>
        private static IRestRequest CreateInitRestRequest(DriveFileUploadRequest request)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveUploadFilesResource, Method.POST, request, true);
            result.AddParameter("X-Upload-Content-Type", request.UploadContentType, ParameterType.HttpHeader);
            result.AddParameter("X-Upload-Content-Length", request.LocalStream.Length, ParameterType.HttpHeader);
            return result;
        }

        /// <summary>
        /// Creates a REST request to upload a file chunk.
        /// </summary>
        /// <param name="uploadContentType">The uploaded content type.</param>
        /// <param name="chunkSize">The file chunk size.</param>
        /// <param name="filePos">The current file position.</param>
        /// <param name="bufferLen">The buffer length.</param>
        /// <param name="fileLen">The file length.</param>
        /// <param name="data">The uploaded data.</param>
        /// <returns>A REST request to upload the specified file chunk.</returns>
        private static IRestRequest CreateUploadRestRequest(string uploadContentType, uint chunkSize, long filePos, int bufferLen, long fileLen, byte[] data)
        {
            string contentRange = string.Format("bytes {0}-{1}/{2}", filePos, filePos + bufferLen - 1, fileLen);
            Log.Debug("CreateUploadRestRequest: Creating a REST request to upload a file chunk. Content-Range: \"{0}\"", contentRange);
            var result = new RestRequest(Method.PUT) {RequestFormat = DataFormat.Json};
            result.AddParameter("Content-Length", chunkSize, ParameterType.HttpHeader)
                .AddParameter("Content-Type", uploadContentType, ParameterType.HttpHeader)
                .AddParameter("Content-Range", contentRange, ParameterType.HttpHeader)
                .AddParameter("application/octet-stream", data, ParameterType.RequestBody);
            return result;
        }

        private static T[] CreateArray<T>(T[] source, int index, int length)
        {
            var result = new T[length];
            Array.Copy(source, index, result, 0, length);
            return result;
        }

        private const HttpStatusCode ResumeIncomplete = (HttpStatusCode) 308;

        #endregion

        #region Logger

        private static NLog.Logger Log
        {
            get { return NLog.LogManager.GetCurrentClassLogger(); }
        }

        #endregion
    }
}