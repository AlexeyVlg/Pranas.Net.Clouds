using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    public class DriveFileListRequest : DriveRequestBase<DriveFileListInfo>
    {
        #region Parameters

        /// <summary>
        /// Gets or sets maximum number of files to return. Acceptable values are 0 to 1000, inclusive. (Default: 100)
        /// </summary>
        [RestParameter("maxResults", ParameterType = ParameterType.GetOrPost)]
        public int? MaxResults { get; set; }

        /// <summary>
        /// Gets or sets the paage token for files.
        /// </summary>
        [RestParameter("pageToken", ParameterType = ParameterType.GetOrPost)]
        public string PageToken { get; set; }

        /// <summary>
        /// Gets or sets the query string for searching files.
        /// See https://developers.google.com/drive/web/search-parameters for more information about supported fields and operations.
        /// </summary>
        [RestParameter("q", ParameterType = ParameterType.GetOrPost)]
        public string Query { get; set; }

        #endregion

        protected override IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveFilesResource, Method.GET, this);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] {HttpStatusCode.OK}); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}