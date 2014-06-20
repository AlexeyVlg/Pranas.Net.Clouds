using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request to move a folder to the trash.
    /// </summary>
    public class DriveFolderTrashRequest : DriveRequestBase<DriveFileInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a request to move a folder to the trash.
        /// </summary>
        /// <param name="folderId">The folder id.</param>
        public DriveFolderTrashRequest(string folderId)
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
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveTrashFilesResource, Method.POST, this);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] {HttpStatusCode.OK}); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}