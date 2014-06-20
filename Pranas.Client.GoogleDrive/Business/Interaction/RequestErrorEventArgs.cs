using Net.Pranas.Client.Common.Business;
using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents arguments of a request error event.
    /// </summary>
    public class RequestErrorEventArgs : EventArgs
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs an instance with arguments of a request error event.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="action">The next action.</param>
        public RequestErrorEventArgs(InteractionException error, RequestErrorAction action)
        {
            Error = error;
            Action = action;
        }

        #endregion

        /// <summary>
        /// Gets the error.
        /// </summary>
        public InteractionException Error { get; private set; }

        /// <summary>
        /// Gets or sets the next action.
        /// </summary>
        public RequestErrorAction Action { get; set; }
    }
}
