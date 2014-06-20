namespace Net.Pranas.Client.GoogleDrive.Business.Defs
{
    /// <summary>
    /// Represents Google Drive message codes.
    /// </summary>
    public static class MessageDefs
    {
        public const string ClientName = "Google Drive";

        public const uint NoLocationHeaderOfUploadResponse = 10001U;

        public const uint InvalidRangeHeaderOfUploadResponse = 10002U;

        public const uint NoRangeHeaderOfUploadResponse = 10003U;
    }
}
