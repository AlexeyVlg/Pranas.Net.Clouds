Pranas.Net.Clouds
=================

This is a lightweight library for working with files on Google Drive.
It was orinially created for http://sqlbak.com service. The library can:
- Connect to Google Drive with OAuth
- Get information about your Google Drive
- List files and folders on Google Drive
- Get information on a Google Drive file
- Upload a file to Google Drive
- Download a file from Google Drive
- Trash/untrash a file on Google Drive
- Delete a file on Google Drive

How to setup Demo application:
-----------------
- Create a Google project using Google API console: https://code.google.com/apis/console/
- Configure credentials for your Google API project to connect with OAuth using Client ID for web application
- Open Pranas.Client.GoogleDrive.Demo.Resources.DriveAuthData class in Pranas.Client.GoogleDrive.Demo project and set "Client ID", "Client secret" and "Redirect URI" from your Google API project. Find TODO items in the class.
- Run Pranas.Client.GoogleDrive.Demo project to upload a file into Google Drive or download a file from Google Drive.

### Code examples
Create a class with authorization data of your Google project:
```csharp
internal class DriveAuthData : IDriveAuthData
{
  public string ClientId
  {
    get { return "your client id"; }
  }
  public string ClientSecret
  {
    get { return "your client secret"; }
  }
  public string RedirectUri
  {
    get { return "your redirect uri"; }
  }
  public string Scope
  {
    get { return "your scope"; }
  }
}
```
Authorize your application and get access token:
```csharp
DriveAuthorization authorization = new DriveAuthorization(new DriveAuthData());
Uri uri = authorization.GetAuthorizationUri(string.Empty, DriveAccessType.Offline, DriveApprovalPrompt.Force);
Process.Start(uri.ToString());
// Get access code from authorization page
string accessCode = "your-access-code";
// Request access token
DriveResponse<DriveTokenInfo> tokenResponse = Authorization.ConfirmAuthorization(accessCode);
DriveTokenInfo driveToken = tokenResponse.Data;
```
Refresh your access token:
```csharp
DriveAuthorization authorization = new DriveAuthorization(new DriveAuthData());
DriveResponse<DriveTokenInfo> tokenResponse = authorization.RefreshToken(driveToken);
DriveTokenInfo driveToken = tokenResponse.Data;
```
Get root folder information:
```csharp
DriveClient client = new DriveClient(new DriveAuthenticator(() => driveToken));
DriveAboutInfo aboutInfo = client.Execute(new DriveAboutRequest()).Data;
DriveFolderInfo rootFolderInfo = new DriveFolderInfo(new DriveFileInfo
  {
    Id = aboutInfo.RootFolderId,
    MimeType = DriveFolderShortInfo.FolderMimeType,
    Title = string.Empty
  }, null);
```
Get a folder information
```csharp
DriveClient client = new DriveClient(new DriveAuthenticator(() => driveToken));
DriveFileListRequest fileListRequest = new DriveFileListRequest
{
  Query = string.Format("mimeType = '{0}' and title = '{1}' and '{2}' in parents " and trashed = {3}",
    DriveFolderShortInfo.FolderMimeType, "folder title", "parent/root folder id", false);
};
DriveResponse<DriveFileListInfo> fileListResponse = client.Execute(fileListRequest);
DriveFileInfo driveFileInfo = fileListResponse.Data.Items != null ? fileListResponse.Data.Items.FirstOrDefault() : null;
```
Get a file information
```csharp
DriveClient client = new DriveClient(new DriveAuthenticator(() => driveToken));
DriveFileListRequest fileListRequest = new DriveFileListRequest
{
  Query = string.Format("mimeType != '{0}' and title = '{1}' and '{2}' in parents " and trashed = {3}",
    DriveFolderShortInfo.FolderMimeType, "folder title", "parent/root folder id", false);
};
DriveResponse<DriveFileListInfo> fileListResponse = client.Execute(fileListRequest);
DriveFileInfo driveFileInfo = fileListResponse.Data.Items != null ? fileListResponse.Data.Items.FirstOrDefault() : null;
```
Create a folder
```csharp
DriveClient client = new DriveClient(new DriveAuthenticator(() => driveToken));
DriveFolderCreateRequest request = new DriveFolderCreateRequest(new DriveFolderShortInfo
{
  Title = "title of new folder",
  Parents = new List<DriveParentReferenceInfo> {new DriveParentReferenceInfo {Id = "parent/root folder id"}}
});
DriveFileInfo driveFileInfo = client.Execute(request).Data;
```
Upload a file
```csharp
DriveClient client = new DriveClient(new DriveAuthenticator(() => driveToken));
FileInfo fileInfo = new FileInfo("path to a local file");
DriveFileInfo driveFileInfo;
using (var fileStream = fileInfo.OpenRead())
{
  var uploadRequest = new DriveFileUploadRequest(fileStream, new DriveFileShortInfo
  {
    Title = "Remote file title / file name",
    Parents = new List<DriveParentReferenceInfo> {new DriveParentReferenceInfo {Id = "parent/root folder id"}}
  });
  uploadRequest.UploadProgress += (sender, args) => Debug.WriteLine("Uploaded {0} of {1}", args.Position, args.Length);
  driveFileInfo = client.Execute(uploadRequest).Data;
}
```
Download a file
```csharp
DriveClient client = new DriveClient(new DriveAuthenticator(() => driveToken));
DriveFileInfo driveFileInfo;
// Gets a drive file information into driveFileInfo
//...
FileInfo fileInfo = new FileInfo("path to a local file");
using (var stream = fileInfo.OpenWrite())
{
  var downloadRequest = new DriveFileDownloadRequest(driveFileInfo, stream);
  downloadRequest.DownloadProgress += Debug.WriteLine("Downloaded {0} of {1}", args.Position, args.Length);
  client.Execute(downloadRequest);
}
```
