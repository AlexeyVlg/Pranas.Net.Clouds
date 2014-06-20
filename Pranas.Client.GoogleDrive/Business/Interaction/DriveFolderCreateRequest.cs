using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request to create a folder.
    /// </summary>
    public class DriveFolderCreateRequest : DriveRequestBase<DriveFileInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs  a request to create a folder.
        /// </summary>
        /// <param name="folderInfo">The folder information.</param>
        public DriveFolderCreateRequest(DriveFolderShortInfo folderInfo)
        {
            if (folderInfo == null)
            {
                throw new ArgumentNullException("folderInfo");
            }

            FolderInfo = folderInfo;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets the folder information.
        /// </summary>
        [RestBody]
        public DriveFolderShortInfo FolderInfo { get; private set; }

        #endregion

        protected override IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveFilesResource, Method.POST, this, true);
            result.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] {HttpStatusCode.OK}); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}