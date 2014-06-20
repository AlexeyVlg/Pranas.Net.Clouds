using Net.Pranas.Client.GoogleDrive.Business.Model;
using Net.Pranas.Client.GoogleDrive.Business.Service;
using RestSharp;
using System;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// The base class of a simple Google Drive request.
    /// </summary>
    /// <typeparam name="T">Drive response type.</typeparam>
    public abstract class DriveRequestBase<T> : IDriveRequest<T> where T : IDriveData, new()
    {
        public DriveResponse<T> Execute(DriveClient driveClient)
        {
            Log.Debug("Execute");

            if (driveClient == null)
            {
                throw new ArgumentNullException("driveClient");
            }

            IRestClient restClient = DoGetRestClient(driveClient);
            IRestRequest restRequest = DoGetRestRequest(driveClient, restClient);
            restRequest.Timeout = Timeout ?? DefaultTimeout;
            HttpStatusCode[] expectedStatusCodes = ExpectedStatusCodes;
            IRestResponse<T> restResponse = RequestHandler.Request<T>(restClient, restRequest, expectedStatusCodes);
            var result = new DriveResponse<T>(restResponse);
            return result;
        }

        /// <summary>
        /// Gets a REST client.
        /// </summary>
        /// <param name="driveClient">The Google Drive client.</param>
        /// <returns>A REST client.</returns>
        protected virtual IRestClient DoGetRestClient(DriveClient driveClient)
        {
            return driveClient.DriveRestClient;
        }

        /// <summary>
        /// Gets a REST request.
        /// </summary>
        /// <param name="driveClient">The Google Drive client.</param>
        /// <param name="restClient">The REST client.</param>
        /// <returns>A REST request.</returns>
        protected abstract IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient);

        /// <summary>
        /// Gets the expected HTTP status codes.
        /// </summary>
        protected abstract HttpStatusCode[] ExpectedStatusCodes { get; }

        /// <summary>
        /// Gets or sets the request timeout.
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// The default request timeout.
        /// </summary>
        public const int DefaultTimeout = 1000*60;

        #region Logger

        private static NLog.Logger Log
        {
            get { return NLog.LogManager.GetCurrentClassLogger(); }
        }

        #endregion
    }
}