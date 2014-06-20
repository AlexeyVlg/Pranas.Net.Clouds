using System;
using System.Net;

namespace Net.Pranas.Client.Common.Business
{
    /// <summary>
    /// Represents error that occus during client interactions.
    /// </summary>
    public class InteractionException : Exception
    {
        #region Constructions and Initializations

        public InteractionException(string clientName, uint errorCode, string message)
            : base(message)
        {
            ClientName = clientName;
            ErrorCode = errorCode;
        }

        public InteractionException(string clientName, uint errorCode, HttpStatusCode statusCode, string statusDescription, string message)
            : this(clientName, errorCode, message)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        #endregion

        /// <summary>
        /// Gets the client name.
        /// </summary>
        public string ClientName { get; private set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public uint ErrorCode { get; private set; }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the HTTP status description.
        /// </summary>
        public string StatusDescription { get; private set; }
    }
}
