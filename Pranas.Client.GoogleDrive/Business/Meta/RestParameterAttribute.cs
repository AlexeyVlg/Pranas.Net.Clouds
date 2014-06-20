using RestSharp;
using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Meta
{
    /// <summary>
    /// Specifies an attribute to describe a property as a REST parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class RestParameterAttribute : Attribute
    {
        #region Construction and Initialization

        public RestParameterAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        #endregion

        /// <summary>
        /// Gets REST parameter name.
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// Gets or sets a REST parameter type.
        /// </summary>
        public ParameterType ParameterType { get; set; }
    }
}
