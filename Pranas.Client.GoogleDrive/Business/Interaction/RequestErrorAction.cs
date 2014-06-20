namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents action on a request error.
    /// </summary>
    public enum RequestErrorAction
    {
        /// <summary>
        /// Throws the current error.
        /// </summary>
        Alert,

        /// <summary>
        /// Retries to execute the current request.
        /// </summary>
        Retry,

        /// <summary>
        /// Skips the error and continue.
        /// </summary>
        Skip
    }
}
