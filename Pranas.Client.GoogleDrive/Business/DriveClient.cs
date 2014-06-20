using Net.Pranas.Client.Common.Business;
using Net.Pranas.Client.GoogleDrive.Business.Auth;
using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Interaction;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System;

namespace Net.Pranas.Client.GoogleDrive.Business
{
    /// <summary>
    /// The Google Drive client.
    /// </summary>
    public class DriveClient
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a Google Drive client.
        /// </summary>
        /// <param name="authenticator">The Google Drive authenticator.</param>
        public DriveClient(DriveAuthenticator authenticator)
        {
            _syncRoot = new object();
            Authenticator = authenticator;
        }

        #endregion

        /// <summary>
        /// Executes a Drive request.
        /// </summary>
        /// <typeparam name="T">The requested data type.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>A response.</returns>
        public DriveResponse<T> Execute<T>(IDriveRequest<T> request) where T : IDriveData, new()
        {
            Log.Debug("Execute.");
            DriveResponse<T> result = null;
            RequestErrorAction errorAction;

            do
            {
                try
                {
                    result = request.Execute(this);
                    break;
                }
                catch (InteractionException exception)
                {
                    Log.TraceException("Execute. Exception", exception);
                    errorAction = RaiseRequestError(exception, RequestErrorAction.Alert);
                    Log.Trace("Execute. NextAction: \"{0}\"", errorAction);

                    if (errorAction == RequestErrorAction.Alert)
                    {
                        throw;
                    }
                }
            } while (errorAction == RequestErrorAction.Retry);
            
            return result;
        }

        #region Drive REST Client

        /// <summary>
        /// Gets Drive REST client related to Drive service location <see cref="ServiceDefs.Drive.ApiLocation"/>.
        /// </summary>
        internal IRestClient DriveRestClient
        {
            get
            {
                if (_driveRestClient == null)
                {
                    lock (_syncRoot)
                    {
                        if (_driveRestClient == null)
                        {
                            _driveRestClient = new RestClient(ServiceDefs.Drive.ApiLocation)
                            {
                                Authenticator = Authenticator.RestAuthenticator,
                                FollowRedirects = false
                            };
                        }
                    }
                }

                return _driveRestClient;
            }
        }

        /// <summary>
        /// The Drive REST client.
        /// </summary>
        private volatile IRestClient _driveRestClient;

        /// <summary>
        /// Creates a new REST client with the current authenticator.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns>A REST client.</returns>
        internal IRestClient CreateRestClient(string baseUrl)
        {
            var result = new RestClient(baseUrl)
            {
                Authenticator = Authenticator.RestAuthenticator,
                FollowRedirects = false
            };
            return result;
        }

        #endregion

        #region RestAuthenticator

        /// <summary>
        /// Gets the Google Drive authenticator.
        /// </summary>
        public DriveAuthenticator Authenticator { get; private set; }

        #endregion

        /// <summary>
        /// Occurs on a request error.
        /// </summary>
        public event EventHandler<RequestErrorEventArgs> RequestError;

        /// <summary>
        /// Raises handlers of RequestError event.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="defAction">The default next action.</param>
        private RequestErrorAction RaiseRequestError(InteractionException error, RequestErrorAction defAction)
        {
            RequestErrorAction result;
            var handler = RequestError;

            if (handler != null)
            {
                var args = new RequestErrorEventArgs(error, defAction);
                handler(this, args);
                result = args.Action;
            }
            else
            {
                result = defAction;
            }

            return result;
        }

        /// <summary>
        /// Gets the whether the client is connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                var tokenInfo = Authenticator.GetTokenInfo();
                return tokenInfo != null && !string.IsNullOrEmpty(tokenInfo.AccessToken);
            }
        }

        private readonly object _syncRoot;

        #region Logger

        private static NLog.Logger Log
        {
            get { return NLog.LogManager.GetCurrentClassLogger(); }
        }

        #endregion
    }
}