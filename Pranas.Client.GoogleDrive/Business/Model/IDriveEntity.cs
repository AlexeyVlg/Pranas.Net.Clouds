namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents common properties of Google Drive entity.
    /// </summary>
    internal interface IDriveEntity
    {
        /// <summary>
        /// Gets or sets the kind of the object.
        /// </summary>
        string Kind { get; set; }
    }
}
