using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents arguments of a data transferring event.
    /// </summary>
    public class DataTransferringEventArgs : EventArgs
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs arguments of a data transferring event.
        /// </summary>
        /// <param name="position">The current position.</param>
        /// <param name="length">The length of the total data.</param>
        public DataTransferringEventArgs(long position, long length)
        {
            Position = position;
            Length = length;
        }

        #endregion

        /// <summary>
        /// Gets the current position.
        /// </summary>
        public long Position { get; private set; }

        /// <summary>
        /// Gets length of the total data.
        /// </summary>
        public long Length { get; private set; }
    }
}
