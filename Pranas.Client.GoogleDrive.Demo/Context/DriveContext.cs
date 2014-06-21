using Net.Pranas.Client.GoogleDrive.Business;
using Net.Pranas.Client.GoogleDrive.Business.Auth;
using Net.Pranas.Client.GoogleDrive.Business.Interaction;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pranas.Client.GoogleDrive.Demo.Resources;

namespace Pranas.Client.GoogleDrive.Demo.Context
{
    /// <summary>
    /// Represens Google Drive context.
    /// </summary>
    public class DriveContext : INotifyPropertyChanged
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a Google Drive context.
        /// </summary>
        public DriveContext()
        {
            _syncRoot = new object();
            _uploadingProgress = 0.0;
            _isUploadingEnabled = true;
            _isDownloadingEnabled = true;
        }

        #endregion

        #region Common Information

        /// <summary>
        /// Gets about information.
        /// </summary>
        public DriveAboutInfo DriveAbout
        {
            get { return _driveAbout; }
            internal set
            {
                if (_driveAbout != value)
                {
                    _driveAbout = value;
                    RaisePropertyChanged("DriveAbout");
                }
            }
        }

        /// <summary>
        /// Drive about information.
        /// </summary>
        private DriveAboutInfo _driveAbout;

        #endregion

        #region File Uploading

        /// <summary>
        /// Uploads a file.
        /// </summary>
        public void UploadFile()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    IsUploadingEnabled = false;
                    IsUploadingInProgress = true;

                    if (AccessToken == null)
                    {
                        throw new InvalidOperationException("The client is not authorized.");
                    }

                    var fileInfo = new FileInfo(PathToLocalUploadedFile);
                    string[] pathItems = PathToRemoteUploadedFile.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);

                    if (pathItems.Length == 0)
                    {
                        throw new InvalidOperationException("Path to remote file is empty");
                    }

                    string fileTitle = pathItems.Last();
                    string[] folderItems = pathItems.Length > 1 ? pathItems.Take(pathItems.Length - 1).ToArray() : null;
                    var folderInfo = GetDriveFolder(Client, DriveAbout.RootFolderId, folderItems, true);

                    using (var fileStream = fileInfo.OpenRead())
                    {
                        var request = new DriveFileUploadRequest(fileStream, new DriveFileShortInfo
                        {
                            Title = fileTitle,
                            Parents = new List<DriveParentReferenceInfo>
                            {
                                new DriveParentReferenceInfo
                                {
                                    Id = folderInfo == null ? DriveAbout.RootFolderId : folderInfo.Id
                                }
                            }
                        });
                        request.UploadProgress += (sender, args) =>
                        {
                            var progress = (args.Position/(double) args.Length)*100;
                            UploadingProgress = progress;
                        };
                        Client.Execute(request);
                    }
                }
                catch (Exception exception)
                {
                    RaiseErrorEvent(exception);
                }
                finally
                {
                    IsUploadingEnabled = true;
                    IsUploadingInProgress = false;
                    UploadingProgress = 0;
                }
            });
        }

        /// <summary>
        /// Gets or sets a path to local file to upload on Google Drive.
        /// </summary>
        public string PathToLocalUploadedFile
        {
            get { return _pathToLocalUploadedFile; }
            set
            {
                if (_pathToLocalUploadedFile != value)
                {
                    _pathToLocalUploadedFile = value;
                    RaisePropertyChanged("PathToLocalUploadedFile");
                }
            }
        }

        /// <summary>
        /// The path to local file to upload on Google Drive.
        /// </summary>
        private string _pathToLocalUploadedFile;

        public string PathToRemoteUploadedFile
        {
            get { return _pathToRemoteUploadedFile; }
            set
            {
                if (_pathToRemoteUploadedFile != value)
                {
                    _pathToRemoteUploadedFile = value;
                    RaisePropertyChanged("PathToRemoteUploadedFile");
                }
            }
        }

        /// <summary>
        /// The path to remote file to upload on Google Drive.
        /// </summary>
        private string _pathToRemoteUploadedFile;

        /// <summary>
        /// Gets a value indicates whether uploading action is enabled.
        /// </summary>
        public bool IsUploadingEnabled
        {
            get { return _isUploadingEnabled; }
            private set
            {
                if (_isUploadingEnabled != value)
                {
                    _isUploadingEnabled = value;
                    RaisePropertyChanged("IsUploadingEnabled");
                }
            }
        }

        private bool _isUploadingEnabled;

        /// <summary>
        /// Gets a value indicates whether uploading action is in progress.
        /// </summary>
        public bool IsUploadingInProgress
        {
            get { return _isUploadingInProgress; }
            private set
            {
                if (_isUploadingInProgress != value)
                {
                    _isUploadingInProgress = value;
                    RaisePropertyChanged("IsUploadingInProgress");
                }
            }
        }

        private bool _isUploadingInProgress;

        /// <summary>
        /// Gets the uploading progress.
        /// </summary>
        public double UploadingProgress
        {
            get { return _uploadingProgress; }
            private set
            {
                if (Math.Abs(_uploadingProgress - value) > 0.01)
                {
                    _uploadingProgress = value;
                    RaisePropertyChanged("UploadingProgress");
                }
            }
        }

        /// <summary>
        /// The uploading progress.
        /// </summary>
        private double _uploadingProgress;

        #endregion

        #region File Downloading

        public void DownloadFile()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    IsDownloadingEnabled = false;
                    IsDownloadingInProgress = true;

                    if (AccessToken == null)
                    {
                        throw new InvalidOperationException("The client is not authorized.");
                    }

                    var fileInfo = new FileInfo(PathToLocalDownloadedFile);

                    if (fileInfo.Exists)
                    {
                        throw new InvalidOperationException(string.Format("File \"{0}\" already exists.", fileInfo.FullName));
                    }

                    string[] pathItems = PathToRemoteDownloadedFile.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);

                    if (pathItems.Length == 0)
                    {
                        throw new InvalidOperationException("Path to remote file is empty");
                    }

                    string fileTitle = pathItems.Last();
                    string[] folderItems = pathItems.Length > 1 ? pathItems.Take(pathItems.Length - 1).ToArray() : null;
                    var folderInfo = GetDriveFolder(Client, DriveAbout.RootFolderId, folderItems);

                    if (folderItems != null && folderInfo == null)
                    {
                        throw new InvalidOperationException(string.Format("Folder \"{0}\" on Google Drive not found", string.Join("/", folderItems)));
                    }

                    var fileListRequest = new DriveFileListRequest
                    {
                        MaxResults = 4,
                        Query = string.Format("mimeType != 'application/vnd.google-apps.folder' and trashed = false and title = '{0}' and '{1}' in parents",
                            fileTitle, folderInfo != null ? folderInfo.Id : DriveAbout.RootFolderId)
                    };

                    var fileListResponse = Client.Execute(fileListRequest);
                    var remoteFileInfo = fileListResponse.Data.Items.FirstOrDefault();

                    if (remoteFileInfo == null)
                    {
                        throw new InvalidOperationException("Remote file not found");
                    }

                    using (var fileStream = fileInfo.OpenWrite())
                    {
                        var downloadRequest = new DriveFileDownloadRequest(remoteFileInfo, fileStream);
                        downloadRequest.DownloadProgress += (sender, args) =>
                        {
                            var progress = (args.Position/(double) args.Length)*100;
                            DownloadingProgress = progress;
                        };
                        Client.Execute(downloadRequest);
                    }
                }
                catch (Exception exception)
                {
                    RaiseErrorEvent(exception);
                }
                finally
                {
                    IsDownloadingEnabled = true;
                    IsDownloadingInProgress = false;
                    DownloadingProgress = 0;
                }
            });
        }

        /// <summary>
        /// Gets or sets a path to remote file to download from Google Drive.
        /// </summary>
        public string PathToRemoteDownloadedFile
        {
            get { return _pathToRemoteDownloadedFile; }
            set
            {
                if (_pathToRemoteDownloadedFile != value)
                {
                    _pathToRemoteDownloadedFile = value;
                    RaisePropertyChanged("PathToRemoteDownloadedFile");
                }
            }
        }

        /// <summary>
        /// The path to remote file to download from Google Drive.
        /// </summary>
        private string _pathToRemoteDownloadedFile;

        /// <summary>
        /// Gets or sets a path to local file to download from Google Drive.
        /// </summary>
        public string PathToLocalDownloadedFile
        {
            get { return _pathToLocalDownloadedFile; }
            set
            {
                if (_pathToLocalDownloadedFile != value)
                {
                    _pathToLocalDownloadedFile = value;
                    RaisePropertyChanged("PathToLocalDownloadedFile");
                }
            }
        }

        /// <summary>
        /// The path to local file to download from Google Drive.
        /// </summary>
        private string _pathToLocalDownloadedFile;

        /// <summary>
        /// Gets a value indicates whether uploading action is enabled.
        /// </summary>
        public bool IsDownloadingEnabled
        {
            get { return _isDownloadingEnabled; }
            private set
            {
                if (_isDownloadingEnabled != value)
                {
                    _isDownloadingEnabled = value;
                    RaisePropertyChanged("IsDownloadingEnabled");
                }
            }
        }

        private bool _isDownloadingEnabled;

        /// <summary>
        /// Gets a value indicates whether downloading action is in progress.
        /// </summary>
        public bool IsDownloadingInProgress
        {
            get { return _isDownloadingInProgress; }
            private set
            {
                if (_isDownloadingInProgress != value)
                {
                    _isDownloadingInProgress = value;
                    RaisePropertyChanged("IsDownloadingInProgress");
                }
            }
        }

        private bool _isDownloadingInProgress;

        /// <summary>
        /// Gets the downloading progress.
        /// </summary>
        public double DownloadingProgress
        {
            get { return _downloadingProgress; }
            private set
            {
                if (Math.Abs(_downloadingProgress - value) > 0.01)
                {
                    _downloadingProgress = value;
                    RaisePropertyChanged("DownloadingProgress");
                }
            }
        }

        /// <summary>
        /// The downloading progress.
        /// </summary>
        private double _downloadingProgress;

        #endregion

        #region Folder Actions

        private static DriveFileInfo GetDriveFolder(DriveClient client, string rootFolderId, IEnumerable<string> pathItems, bool createIfNotExists = false)
        {
            DriveFileInfo currentFolder = null;
            string currentId = rootFolderId;

            foreach (var pathItem in pathItems)
            {
                var getRequest = new DriveFileListRequest
                {
                    MaxResults = 4,
                    Query = string.Format("trashed = false and title = '{0}' and mimeType = 'application/vnd.google-apps.folder'", pathItem)
                };
                var getResponse = client.Execute(getRequest);
                currentFolder = getResponse.Data.Items.FirstOrDefault();

                if (currentFolder == null)
                {
                    if (!createIfNotExists)
                    {
                        break;
                    }

                    var createRequest = new DriveFolderCreateRequest(new DriveFolderShortInfo
                    {
                        Title = pathItem,
                        Parents = new List<DriveParentReferenceInfo>
                        {
                            new DriveParentReferenceInfo {Id = currentId}
                        }
                    });
                    var createResponse = client.Execute(createRequest);
                    currentFolder = createResponse.Data;
                }
                currentId = currentFolder.Id;
            }

            return currentFolder;
        }

        #endregion

        #region Drive Client

        /// <summary>
        /// Gets Google Drive client.
        /// </summary>
        internal DriveClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (_syncRoot)
                    {
                        if (_client == null)
                        {
                            _client = new DriveClient(new DriveAuthenticator(() => AccessToken));
                        }
                    }
                }

                return _client;
            }
        }

        /// <summary>
        /// Google Drive client.
        /// </summary>
        private volatile DriveClient _client;

        #endregion

        #region OAuth Token

        /// <summary>
        /// Gets the Drive connection token.
        /// </summary>
        public DriveTokenInfo AccessToken
        {
            get
            {
                if (_accessToken != null && _accessToken.ExpiresAt != null)
                {
                    var timeSpan = _accessToken.ExpiresAt.Value - DateTime.UtcNow;

                    // Refreshes access token if it expires in 10 minutes.
                    if (timeSpan.TotalMinutes < 10)
                    {
                        lock (_syncRoot)
                        {
                            var timeSpan1 = _accessToken.ExpiresAt.Value - DateTime.UtcNow;

                            if (timeSpan1.TotalMinutes < 10)
                            {
                                try
                                {
                                    var authorization = new DriveAuthorization(new DriveAuthData());
                                    _accessToken = authorization.RefreshToken(_accessToken).Data;
                                }
                                catch (Exception exception)
                                {
                                    RaiseErrorEvent(exception);
                                }
                            }
                        }
                    }
                }

                return _accessToken;
            }
            internal set
            {
                if (_accessToken != value)
                {
                    _accessToken = value;
                    AccessTokenCreatedAt = _accessToken != null ? _accessToken.CreatedAt.ToLocalTime() : (DateTime?) null;
                    AccessTokenExpiresAt = _accessToken != null && _accessToken.ExpiresAt != null
                        ? _accessToken.ExpiresAt.Value.ToLocalTime()
                        : (DateTime?) null;
                    RaisePropertyChanged("AccessToken");
                }
            }
        }

        /// <summary>
        /// The Drive access token.
        /// </summary>
        private DriveTokenInfo _accessToken;

        /// <summary>
        /// Gets the access token created time.
        /// </summary>
        public DateTime? AccessTokenCreatedAt
        {
            get { return _accessTokenCreatedAt; }
            private set
            {
                if (_accessTokenCreatedAt != value)
                {
                    _accessTokenCreatedAt = value;
                    RaisePropertyChanged("AccessTokenCreatedAt");
                }
            }
        }

        /// <summary>
        /// The access token created time.
        /// </summary>
        private DateTime? _accessTokenCreatedAt;

        /// <summary>
        /// Gets the access token expiration time.
        /// </summary>
        public DateTime? AccessTokenExpiresAt
        {
            get { return _accessTokenExpiresAt; }
            private set
            {
                if (_accessTokenExpiresAt != value)
                {
                    _accessTokenExpiresAt = value;
                    RaisePropertyChanged("AccessTokenExpiresAt");
                }
            }
        }

        /// <summary>
        /// The access token expiration time.
        /// </summary>
        private DateTime? _accessTokenExpiresAt;

        #endregion

        /// <summary>
        /// Occurs on context error.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Error;

        /// <summary>
        /// Raises handlers of Error event.
        /// </summary>
        /// <param name="error">The error.</param>
        private void RaiseErrorEvent(Exception error)
        {
            var handler = Error;

            if (handler != null)
            {
                handler(this, new ExceptionEventArgs(error));
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises handlers of PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected internal void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private readonly object _syncRoot;
    }
}