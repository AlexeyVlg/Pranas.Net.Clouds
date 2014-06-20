using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request to get a file's metadata.
    /// </summary>
    public class DriveFileGetRequest : DriveRequestBase<DriveFileInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a request to get a file's metadata.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        public DriveFileGetRequest(string fileId)
        {
            FileId = fileId;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets the file id.
        /// </summary>
        [RestParameter("fileId", ParameterType = ParameterType.UrlSegment)]
        public string FileId { get; private set; }

        /// <summary>
        /// Gets or sets the whether to update the view date after successfully retrieving the file. (Default: false)
        /// </summary>
        [RestParameter("updateViewedDate", ParameterType = ParameterType.GetOrPost)]
        public bool? UpdateViewedDate { get; set; }

        #endregion

        protected override IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveFilesResource, Method.GET, this);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] {HttpStatusCode.OK, HttpStatusCode.NotFound}); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}