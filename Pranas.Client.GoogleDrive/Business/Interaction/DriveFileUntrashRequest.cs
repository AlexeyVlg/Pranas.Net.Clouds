using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request to restore a file from the trash.
    /// </summary>
    public class DriveFileUntrashRequest : DriveRequestBase<DriveFileInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a request to restore a file from the trash.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        public DriveFileUntrashRequest(string fileId)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            FileId = fileId;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets the file id.
        /// </summary>
        [RestParameter(ServiceDefs.Drive.FileIdParameterName, ParameterType = ParameterType.UrlSegment)]
        public string FileId { get; private set; }

        #endregion

        protected override IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveUntrashFilesResource, Method.POST, this);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] { HttpStatusCode.OK }); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}
