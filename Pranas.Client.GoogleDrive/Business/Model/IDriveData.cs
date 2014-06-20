namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a Google Drive data object.
    /// </summary>
    public interface IDriveData
    {
        /// <summary>
        /// Gets or sets an error code string.
        /// </summary>
        string Error { get; set; }

        /// <summary>
        /// Gets or sets an error description text.
        /// </summary>
        string ErrorDescription { get; set; }
    }
}
