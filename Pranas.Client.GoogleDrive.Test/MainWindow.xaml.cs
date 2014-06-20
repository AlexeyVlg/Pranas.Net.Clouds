using Microsoft.Win32;
using Net.Pranas.Client.GoogleDrive.Business.Auth;
using Pranas.Client.GoogleDrive.Test.Context;
using Pranas.Client.GoogleDrive.Test.Controls;
using Pranas.Client.GoogleDrive.Test.Resources;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Pranas.Client.GoogleDrive.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Construction and Initialization

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Window Events

        /// <summary>
        /// Occurs on the window is loaded.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            var context = DataContext as DriveContext;
            Debug.Assert(context != null, "context != null");
            context.Error += (o, args) => ShowErrorMessage(args.Error);
        }

        #endregion

        /// <summary>
        /// Occurs on click on a button to connext to Google Drive.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DriveConnectButtonClick(object sender, RoutedEventArgs e)
        {
            var driveConnectWindow = new DriveConnectWindow
            {
                Owner = this
            };
            driveConnectWindow.ShowDialog();
        }

        /// <summary>
        /// Occurs on click on a button to refresh access token.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void RefreshTokenButtonClick(object sender, RoutedEventArgs e)
        {
            var authorization = new DriveAuthorization(new DriveAuthData());
            var context = DataContext as DriveContext;
            Debug.Assert(context != null, "context != null");
            Task.Factory.StartNew(() =>
            {
                try
                {
                    context.AccessToken = authorization.RefreshToken(context.AccessToken).Data;
                }
                catch (Exception exception)
                {
                    Dispatcher.BeginInvoke(new Action<Exception>(ShowErrorMessage), exception);
                }
            });
        }

        /// <summary>
        /// Occurs on click on a button to revoke access token.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void RevokeTokenButtonClick(object sender, RoutedEventArgs e)
        {
            var authorization = new DriveAuthorization(new DriveAuthData());
            var context = DataContext as DriveContext;
            Debug.Assert(context != null, "context != null");
            Task.Factory.StartNew(() =>
            {
                try
                {
                    authorization.RevokeToken(context.AccessToken);
                    context.AccessToken = null;
                    context.DriveAbout = null;
                }
                catch (Exception exception)
                {
                    Dispatcher.BeginInvoke(new Action<Exception>(ShowErrorMessage), exception);
                }
            });
        }

        private void ShowErrorMessage(Exception exception)
        {
            Dispatcher.BeginInvoke(new Action<Exception>(error => MessageBox.Show(this, error.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error)), exception);
        }

        #region File Uploading

        /// <summary>
        /// Occurs on click on a button to choose a file to upload.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ChooseUploadFileButtonClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as DriveContext;
            Debug.Assert(context != null, "context != null");

            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false
            };

            if (!string.IsNullOrEmpty(context.PathToLocalUploadedFile))
            {
                string path = Path.GetDirectoryName(context.PathToLocalUploadedFile);

                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    dialog.InitialDirectory = path;
                }

                string fileName = Path.GetFileName(context.PathToLocalUploadedFile);

                if (!string.IsNullOrEmpty(fileName))
                {
                    dialog.FileName = fileName;
                }
            }

            if (dialog.ShowDialog(this) == true)
            {
                context.PathToLocalUploadedFile = dialog.FileName;
            }
        }

        /// <summary>
        /// Occurs on click on a button to upload a file.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void UploadButtonClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as DriveContext;
            Debug.Assert(context != null, "context != null");
            context.UploadFile();
        }

        #endregion

        #region File Downloading

        /// <summary>
        /// Occurs on click on a button to choose a file to download.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ChooseDownloadFileButtonClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as DriveContext;
            Debug.Assert(context != null, "context != null");

            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                Multiselect = false
            };

            if (!string.IsNullOrEmpty(context.PathToLocalDownloadedFile))
            {
                string path = Path.GetDirectoryName(context.PathToLocalDownloadedFile);

                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    dialog.InitialDirectory = path;
                }

                string fileName = Path.GetFileName(context.PathToLocalDownloadedFile);

                if (!string.IsNullOrEmpty(fileName))
                {
                    dialog.FileName = fileName;
                }
            }

            if (dialog.ShowDialog(this) == true)
            {
                context.PathToLocalDownloadedFile = dialog.FileName;
            }
        }

        /// <summary>
        /// Occurs on click on a button to download a file from Google Drive.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DownloadButtonClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as DriveContext;
            Debug.Assert(context != null, "context != null");
            context.DownloadFile();
        }

        #endregion
    }
}
