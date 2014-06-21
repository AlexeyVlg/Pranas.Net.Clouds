using System;

namespace Pranas.Client.GoogleDrive.Demo.Context
{
    /// <summary>
    /// Represents arguments of an exception event.
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs an arguments of an exception event.
        /// </summary>
        /// <param name="error">The error.</param>
        public ExceptionEventArgs(Exception error)
        {
            Error = error;
        }

        #endregion

        /// <summary>
        /// Gets the error.
        /// </summary>
        public Exception Error { get; private set; }
    }
}
