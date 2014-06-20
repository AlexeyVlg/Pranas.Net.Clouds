using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;

namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Indicates whether your application needs to access a Google API when the user is not present at the browser.
    /// This will result in your application obtaining a refresh token the first time your application exchanges an authorization code for a user.
    /// </summary>
    public enum DriveAccessType
    {
        [StringValue(ServiceDefs.Auth.AccessTypeOnlineParameterValue)]
        Online,

        [StringValue(ServiceDefs.Auth.AccessTypeOfflineParameterValue)]
        Offline
    }
}
