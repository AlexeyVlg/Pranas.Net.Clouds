using System;
using System.Linq;
using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using Net.Pranas.Client.GoogleDrive.Business.Serialization;
using Net.Pranas.Client.GoogleDrive.Resources;
using RestSharp;
using System.Collections.Generic;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a factory of REST requests.
    /// </summary>
    internal static class RestRequestFactory
    {
        /// <summary>
        /// Creates a REST request with Drive API version parameter.
        /// </summary>
        /// <param name="resource">The request location.</param>
        /// <param name="method">The request method.</param>
        /// <returns>A REST request.</returns>
        internal static IRestRequest CreateRestRequest(string resource, Method method)
        {
            var result = new RestRequest(resource, method)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new RestSharpDataContractJsonSerializer()
            };
            result.AddParameter(ServiceDefs.Drive.ApiVersionParameterName, ServiceDefs.Drive.ApiVersion, ParameterType.UrlSegment);
            return result;
        }

        /// <summary>
        /// Creates a REST request with Drive API version parameter.
        /// </summary>
        /// <param name="resource">The request location.</param>
        /// <param name="method">The request method.</param>
        /// <param name="parameters">The additional parameters.</param>
        /// <returns>A REST request.</returns>
        internal static IRestRequest CreateRestRequest(string resource, Method method, IEnumerable<Parameter> parameters)
        {
            var result = CreateRestRequest(resource, method);
            result.Parameters.AddRange(parameters);
            return result;
        }

        /// <summary>
        /// Creates a REST request with Drive API version parameter.
        /// </summary>
        /// <typeparam name="T">The type of a retrieved data object.</typeparam>
        /// <param name="resource">The request location.</param>
        /// <param name="method">The request method.</param>
        /// <param name="driveRequest">The Drive reqiest.</param>
        /// <param name="hasBody">The whether if drive request has body.</param>
        /// <returns>A REST request.</returns>
        internal static IRestRequest CreateRestRequest<T>(string resource, Method method, IDriveRequest<T> driveRequest, bool hasBody = false)
            where T : IDriveData, new()
        {
            object bodyObject;

            if (hasBody)
            {
                bodyObject = RestParametersHelper.GetBodyObjects(driveRequest).FirstOrDefault();

                if (bodyObject == null)
                {
                    throw new InvalidOperationException(LocalStrings.RequestBodyNullReferenceErrorMessage);
                }
            }
            else
            {
                bodyObject = null;
            }

            var result = CreateRestRequest(resource, method, RestParametersHelper.GetParameters(driveRequest));

            if (hasBody)
            {
                result.AddBody(bodyObject);
            }

            return result;
        }
    }
}