using Net.Pranas.Client.GoogleDrive.Business.Auth;
using Net.Pranas.Client.GoogleDrive.Business.Interaction;
using Pranas.Client.GoogleDrive.Demo.Context;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Pranas.Client.GoogleDrive.Demo.Resources;

namespace Pranas.Client.GoogleDrive.Demo.Controls
{
    /// <summary>
    /// Interaction logic for DriveConnectWindow.xaml
    /// </summary>
    public partial class DriveConnectWindow
    {
        #region Construction and Initialization

        public DriveConnectWindow()
        {
            InitializeComponent();
            Authorization = new DriveAuthorization(new DriveAuthData());
        }

        #endregion

        /// <summary>
        /// Occurs on click on a button to open Google Drive connection page.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OpenConnectionPageButtonClick(object sender, RoutedEventArgs e)
        {
            Uri uri = Authorization.GetAuthorizationUri(string.Empty, DriveAccessType.Offline, DriveApprovalPrompt.Force);
            Process.Start(uri.ToString());
        }

        /// <summary>
        /// Occurs on click on a button to paste access code from clipboard.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void PasteAccessCodeButtonClick(object sender, RoutedEventArgs e)
        {
            AccessCodeTextBox.Text = Clipboard.GetText();
        }

        /// <summary>
        /// Occurs on click on a button to connect to Google Drive.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ConnectButtonClick(object sender, RoutedEventArgs e)
        {
            string accessCode = AccessCodeTextBox.Text;

            try
            {
                var tokenResponse = Authorization.ConfirmAuthorization(accessCode);
                var driveContext = DataContext as DriveContext;
                Debug.Assert(driveContext != null, "dataModel != null");
                driveContext.AccessToken = tokenResponse.Data;
                Task.Factory.StartNew(() => { driveContext.DriveAbout = driveContext.Client.Execute(new DriveAboutRequest()).Data; });
                DialogResult = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets the instance of Google Drive authorization.
        /// </summary>
        protected DriveAuthorization Authorization { get; private set; }
    }
}