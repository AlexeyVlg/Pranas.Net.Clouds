using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Meta;

namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Indicates whether the user should be re-prompted for consent.
    /// If the value is force, then the user sees a consent page even if they previously gave consent to your application for a given set of scopes.
    /// </summary>
    public enum DriveApprovalPrompt
    {
        [StringValue(ServiceDefs.Auth.ApprovalPromptAutoParameterValue)]
        Auto,

        [StringValue(ServiceDefs.Auth.ApprovalPromptForceParameterValue)]
        Force
    }
}