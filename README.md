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
