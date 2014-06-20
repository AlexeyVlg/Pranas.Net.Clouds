using Net.Pranas.Client.GoogleDrive.Business.Model;
using Net.Pranas.Client.GoogleDrive.Business.Service;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request to download a file.
    /// </summary>
    public class DriveFileDownloadRequest : IDriveRequest<DriveEmptyDataInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a request to download a Google Drive file.
        /// </summary>
        /// <param name="fileId">The remote file id.</param>
        /// <param name="responseWriter">The stream to wirte a file content.</param>
        public DriveFileDownloadRequest(string fileId, Stream responseWriter)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (responseWriter == null)
            {
                throw new ArgumentNullException("responseWriter");
            }

            FileId = fileId;
            ResponseWriter = responseWriter;
        }

        /// <summary>
        /// Constructs a request to download a Google Drive file.
        /// </summary>
        /// <param name="driveFile">The remote file information.</param>
        /// <param name="responseWriter">The stream to wirte a file content.</param>
        public DriveFileDownloadRequest(DriveFileInfo driveFile, Stream responseWriter)
        {
            if (driveFile == null)
            {
                throw new ArgumentNullException("driveFile");
            }

            if (responseWriter == null)
            {
                throw new ArgumentNullException("responseWriter");
            }

            DriveFile = driveFile;
            ResponseWriter = responseWriter;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets the remote file id.
        /// </summary>
        public string FileId { get; private set; }

        /// <summary>
        /// Gets the remote file information.
        /// </summary>
        public DriveFileInfo DriveFile { get; private set; }

        /// <summary>
        /// Gets or sets the chunk size.
        /// </summary>
        public uint? ChunkSize { get; set; }

        /// <summary>
        /// The default chunk size;
        /// </summary>
        public const uint DefaultChunkSize = 256*1024*16;

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
        /// Occurs on downloading data progress changed.
        /// </summary>
        public event EventHandler<DataTransferringEventArgs> DownloadProgress;

        /// <summary>
        /// Raises handlers of DownloadProgress event.
        /// </summary>
        /// <param name="position">The current position.</param>
        /// <param name="length">The length of the total data.</param>
        private void RaiseDownloadProgress(long position, long length)
        {
            var handler = DownloadProgress;

            if (handler != null)
            {
                handler(this, new DataTransferringEventArgs(position, length));
            }
        }

        #endregion

        /// <summary>
        /// Gets the stream to wirte a file content.
        /// </summary>
        public Stream ResponseWriter { get; private set; }

        public DriveResponse<DriveEmptyDataInfo> Execute(DriveClient driveClient)
        {
            var log = Log;
            log.Debug("Execute: Downloading a file.");
            var driveFile = DriveFile;

            if (driveFile == null)
            {
                var request = new DriveFileGetRequest(FileId);
                var response = driveClient.Execute(request);
                DriveFile = driveFile = response.Data;
            }

            var uriItems = driveFile.DownloadUrl.Split(new[] {'?'}, StringSplitOptions.RemoveEmptyEntries);

            IRestResponse restResponse;
            long rangeBegin = 0;
            uint chunkSize = ChunkSize ?? DefaultChunkSize;
            var responseWriter = ResponseWriter;
            long fileSize = driveFile.FileSize;
            RaiseDownloadProgress(0, fileSize);
            IRestClient restClient = driveClient.CreateRestClient(uriItems[0]);
            int timeout = WriteChunkTimeout ?? DefaultWriteChunkTimeout;

            do
            {
                IRestRequest restRequest = CreateRestRequest(uriItems.Length > 1 ? uriItems[1] : string.Empty);
                restRequest.Timeout = timeout;
                long rangeEnd = Math.Min(rangeBegin + chunkSize, fileSize - 1);
                string rangeStr = string.Format("bytes={0}-{1}", rangeBegin, rangeEnd);
                log.Trace("Execute: Downloading a chunk of file. FilePos: \"{0}\"; BufferLen: \"{1}\"; FileLen: \"{2}\"", rangeBegin, rangeEnd - rangeBegin, fileSize);
                restRequest.AddParameter("Range", rangeStr, ParameterType.HttpHeader);
                restRequest.ResponseWriter = stream => stream.CopyTo(responseWriter);
                restResponse = RequestHandler.Request(restClient, restRequest, HttpStatusCode.OK, HttpStatusCode.PartialContent);

                if (restResponse.StatusCode == HttpStatusCode.PartialContent)
                {
                    uriItems = restResponse.ResponseUri.ToString().Split(new[] {'?'}, StringSplitOptions.RemoveEmptyEntries);
                    string rangeResultStr = GetUploadedRange(restResponse, ref rangeBegin, ref rangeEnd, ref fileSize);
                    log.Trace("Execute: Chunk downloaded. Content-Range: \"{0}\"", rangeResultStr);
                    RaiseDownloadProgress(rangeEnd, fileSize);
                    rangeBegin = rangeEnd + 1;

                    if (rangeBegin >= fileSize)
                    {
                        break;
                    }
                }

                restClient.BaseUrl = uriItems[0];
            } while (restResponse.StatusCode == HttpStatusCode.PartialContent);

            var result = new DriveResponse<DriveEmptyDataInfo>(restResponse);
            return result;
        }

        /// <summary>
        /// Creates a REST request to download a file.
        /// </summary>
        /// <param name="queryString">The query string of a file downloading URI resource.</param>
        /// <returns>A REST request.</returns>
        private static IRestRequest CreateRestRequest(string queryString)
        {
            var result = new RestRequest(Method.GET);
            string[] queryItems = queryString.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryItem in queryItems)
            {
                var paramItems = queryItem.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);

                if (paramItems.Length == 2)
                {
                    result.AddParameter(paramItems[0], paramItems[1], ParameterType.QueryString);
                }
            }

            return result;
        }

        /// <summary>
        /// Extracts downloaded range from a REST response.
        /// </summary>
        /// <param name="restResponse">The REST response.</param>
        /// <param name="rangeBegin">The range begin value.</param>
        /// <param name="rangeEnd">The range end value.</param>
        /// <param name="fileSize">The file size.</param>
        public static string GetUploadedRange(IRestResponse restResponse, ref long rangeBegin, ref long rangeEnd, ref long fileSize)
        {
            const string contentRangeHeaderName = "Content-Range";
            var header = restResponse.Headers.FirstOrDefault(x => x.Name == contentRangeHeaderName);
            string headerValue;

            if (header != null)
            {
                headerValue = header.Value as string;

                if (!string.IsNullOrEmpty(headerValue))
                {
                    const string regExpression = @"bytes (?'begin'\d{1,})-(?'end'\d{1,})\/(?'total'\d{1,})";
                    Match m = Regex.Match(headerValue, regExpression);

                    if (m.Success)
                    {
                        var beginGroup = m.Groups["begin"];
                        string beginStr = beginGroup != null && beginGroup.Success ? beginGroup.Value : null;
                        if (!string.IsNullOrEmpty(beginStr))
                        {
                            long.TryParse(beginStr, out rangeBegin);
                        }

                        var endGroup = m.Groups["end"];
                        string endStr = endGroup != null && endGroup.Success ? endGroup.Value : null;
                        if (!string.IsNullOrEmpty(endStr))
                        {
                            long.TryParse(endStr, out rangeEnd);
                        }

                        var totalGroup = m.Groups["total"];
                        string totalStr = totalGroup != null && totalGroup.Success ? totalGroup.Value : null;
                        if (!string.IsNullOrEmpty(totalStr))
                        {
                            long.TryParse(totalStr, out fileSize);
                        }
                    }
                }
            }
            else
            {
                headerValue = string.Empty;
            }

            return headerValue;
        }

        #region Logger

        private static NLog.Logger Log
        {
            get { return NLog.LogManager.GetCurrentClassLogger(); }
        }

        #endregion
    }
}