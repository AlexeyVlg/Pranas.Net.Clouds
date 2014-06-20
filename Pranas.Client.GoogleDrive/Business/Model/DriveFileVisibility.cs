using Net.Pranas.Client.GoogleDrive.Business.Meta;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a file visibility values.
    /// </summary>
    public enum DriveFileVisibility
    {
        [StringValue("DEFAULT")]
        Default,

        [StringValue("PRIVATE")]
        Private
    }
}
