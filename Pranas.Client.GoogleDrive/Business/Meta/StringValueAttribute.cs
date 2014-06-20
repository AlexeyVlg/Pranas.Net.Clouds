using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Meta
{
    /// <summary>
    /// Represents an attribute with a string value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class StringValueAttribute : Attribute
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs an attribute with a specified string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        public StringValueAttribute(string value)
        {
            Value = value;
        }

        #endregion

        /// <summary>
        /// Gets the string value.
        /// </summary>
        public string Value { get; private set; }
    }
}
