using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request to restore a folder from the trash.
    /// </summary>
    public class DriveFolderUntrashRequest : DriveRequestBase<DriveFileInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a request to restore a folder from the trash.
        /// </summary>
        /// <param name="folderId">The folder id.</param>
        public DriveFolderUntrashRequest(string folderId)
        {
            if (folderId == null)
            {
                throw new ArgumentNullException("folderId");
            }

            FolderId = folderId;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets the folder id.
        /// </summary>
        [RestParameter(ServiceDefs.Drive.FileIdParameterName, ParameterType = ParameterType.UrlSegment)]
        public string FolderId { get; private set; }

        #endregion

        protected override IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveUntrashFilesResource, Method.POST, this);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] {HttpStatusCode.OK}); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}