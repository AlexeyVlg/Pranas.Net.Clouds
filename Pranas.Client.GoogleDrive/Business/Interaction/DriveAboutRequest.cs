using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a request of a user information and settings.
    /// </summary>
    public class DriveAboutRequest : DriveRequestBase<DriveAboutInfo>
    {
        protected override IRestRequest DoGetRestRequest(DriveClient driveClient, IRestClient restClient)
        {
            var result = RestRequestFactory.CreateRestRequest(ServiceDefs.Drive.DriveAboutResource, Method.GET);
            return result;
        }

        protected override HttpStatusCode[] ExpectedStatusCodes
        {
            get { return _expectedStatusCodes ?? (_expectedStatusCodes = new[] {HttpStatusCode.OK}); }
        }

        private HttpStatusCode[] _expectedStatusCodes;
    }
}