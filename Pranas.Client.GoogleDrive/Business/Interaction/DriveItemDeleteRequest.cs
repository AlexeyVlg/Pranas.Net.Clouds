using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a requets to delete a file or a folder.
    /// </summary>
    public class DriveItemDeleteRequest : DriveRequestBase<DriveEmptyDataInfo>
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a request to delete a file with a specified item id.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        public DriveItemDeleteRequest(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException("itemId");
            }

            ItemId = itemId;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets the file id.
        /// </summary>
        [RestParameter(ServiceDefs.Drive.FileIdParameterName, ParameterType = ParameterType.UrlSegment)]
        public string ItemId { get; private set; }

        #endregion

        protected override IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveDeleteFilesResource, Method.DELETE, this);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] { HttpStatusCode.NoContent }); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}
