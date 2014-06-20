using Net.Pranas.Client.Common.Business;
using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Resources;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Threading;

namespace Net.Pranas.Client.GoogleDrive.Business.Service
{
    /// <summary>
    /// Represents a global handler of requests to Google Drive service.
    /// </summary>
    internal static class RequestHandler
    {
        /// <summary>
        /// Requests a data.
        /// </summary>
        /// <typeparam name="T">The requested data type.</typeparam>
        /// <param name="client">The REST client.</param>
        /// <param name="request">The REST request.</param>
        /// <param name="statusCode">The expected HTTP status code.</param>
        /// <returns>A REST response.</returns>
        internal static IRestResponse<T> Request<T>(IRestClient client, IRestRequest request, params HttpStatusCode[] statusCode) where T : new()
        {
            IRestResponse<T> result = null;
            Request(client, request, (restClient, restRequest) => (result = restClient.Execute<T>(restRequest)), statusCode);
            return result;
        }

        /// <summary>
        /// Requests a data.
        /// </summary>
        /// <param name="client">The REST client.</param>
        /// <param name="request">The REST request.</param>
        /// <param name="statusCode">The expected HTTP status code.</param>
        /// <returns>A REST response.</returns>
        internal static IRestResponse Request(IRestClient client, IRestRequest request, params HttpStatusCode[] statusCode)
        {
            IRestResponse result = null;
            Request(client, request, (restClient, restRequest) => (result = restClient.Execute(restRequest)), statusCode);
            return result;
        }

        /// <summary>
        /// Requests a data.
        /// </summary>
        /// <param name="client">The REST client.</param>
        /// <param name="request">The REST request.</param>
        /// <param name="statusCode">The expected HTTP status code.</param>
        /// <param name="executeRequest">The function to execute the request and return a response.</param>
        private static void Request(IRestClient client, IRestRequest request,
            Func<IRestClient, IRestRequest, IRestResponse> executeRequest, params HttpStatusCode[] statusCode)
        {
            var log = Log;
            log.Debug("Execute: Sending a request. BaseUrl: \"{0}\"; Resource: \"{1}\"", client.BaseUrl, request.Resource);
            IRestResponse response = null;

            for (byte attempt = 0; attempt < AttemptCount; attempt++)
            {
                response = executeRequest(client, request);
                var codeResult = (int) response.StatusCode;

                if (codeResult/100 == 5)
                {
                    log.Trace("Execute: Received a data with \"{0}\" code. Status: \"{1}\"; StatusDescription: \"{2}\"; Content: \"{3}\"",
                        codeResult, response.StatusCode, response.StatusDescription, response.Content);

                    if (attempt < AttemptCount - 1)
                    {
                        TimeSpan ts = GetDelay(attempt);
                        log.Trace("Execute: Trying again after \"{0}\" delay", ts);
                        Thread.Sleep(ts);
                    }
                }
                else
                {
                    log.Trace("Execute: Received. StatusCode: \"{0}\"", codeResult);
                    break;
                }
            }

            Exception exception = ValidateRestResponse(client, request, response, statusCode);

            if (exception != null)
            {
                if (response != null)
                {
                    log.Trace("Execute: Received a data with \"{0}\" code. Status: \"{1}\"; StatusDescription: \"{2}\"; Content: \"{3}\"",
                        response.StatusCode, response.StatusDescription, response.Content);
                }
                else
                {
                    log.Trace("Execute: Response is null");
                }

                throw exception;
            }
        }

        /// <summary>
        /// Validates a REST response with a specified HTTP status code.
        /// </summary>
        /// <param name="client">The REST client.</param>
        /// <param name="request">The REST request.</param>
        /// <param name="response">The REST response.</param>
        /// <param name="statusCode">The expected HTTP status code.</param>
        /// <returns>An exception instance or null value if the response is correct.</returns>
        private static Exception ValidateRestResponse(IRestClient client, IRestRequest request, IRestResponse response, params HttpStatusCode[] statusCode)
        {
            Exception result;

            if (response == null)
            {
                result = new InvalidOperationException(string.Format(LocalStrings.ResponseNullReferenceErrorMessage1, client.BuildUri(request)));
            }
            else if (response.ErrorException != null)
            {
                result = response.ErrorException;
            }
            else if (!statusCode.Any(x => x == response.StatusCode))
            {
                result = new InteractionException(MessageDefs.ClientName, CommonMessageDefs.UnexpectedHttpStatusCode,
                    response.StatusCode, response.StatusDescription,
                    string.Format(LocalStrings.ResponseStatusCodeUnexpectedErrorMessage2, response.StatusCode, response.StatusDescription));
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Gets a delay interval.
        /// </summary>
        /// <param name="attempt">Current attempt number.</param>
        /// <returns>A delay time interval.</returns>
        private static TimeSpan GetDelay(byte attempt)
        {
            var rnd = new Random();
            int ms = rnd.Next(1, 1000);
            var delaySeconds = (int) Math.Pow(2, attempt);
            var result = new TimeSpan(0, 0, 0, delaySeconds, ms);
            return result;
        }

        private const byte AttemptCount = 6;

        #region Logger

        private static NLog.Logger Log
        {
            get { return NLog.LogManager.GetCurrentClassLogger(); }
        }

        #endregion
    }
}