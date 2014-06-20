using Net.Pranas.Client.GoogleDrive.Business.Model;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a REST request.
    /// </summary>
    /// <typeparam name="T">The type of a retrieved data object.</typeparam>
    public interface IDriveRequest<T> where T : IDriveData, new()
    {
        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="driveClient">The Google Drive client.</param>
        /// <returns>A response.</returns>
        DriveResponse<T> Execute(DriveClient driveClient);
    }
}
