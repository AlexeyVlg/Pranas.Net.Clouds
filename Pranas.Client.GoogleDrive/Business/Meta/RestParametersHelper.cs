using System;
using Net.Pranas.Client.GoogleDrive.Business.Interaction;
using Net.Pranas.Client.GoogleDrive.Business.Meta.Ext;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Net.Pranas.Client.GoogleDrive.Business.Meta
{
    /// <summary>
    /// Represents a helper class for REST parameters.
    /// </summary>
    internal static class RestParametersHelper
    {
        /// <summary>
        /// Gets parameters for a REST request from a Google Drive request.
        /// </summary>
        /// <typeparam name="T">The type of a retrieved data object.</typeparam>
        /// <param name="request">The Google Drive request.</param>
        /// <returns>REST request parameters.</returns>
        internal static IEnumerable<Parameter> GetParameters<T>(IDriveRequest<T> request) where T : IDriveData, new()
        {
            var attrType = typeof (RestParameterAttribute);
            var properties = request.GetType().GetProperties()
                .Where(p => p.IsDefined(attrType, false))
                .Select(p => new {Property = p, Attr = p.GetCustomAttributes(attrType, false).OfType<RestParameterAttribute>().FirstOrDefault()})
                .Where(x => x.Attr != null);

            foreach (var property in properties)
            {
                string strValue;
                if (property.Property.PropertyType.IsEnum)
                {
                    var eVal = property.Property.GetValue(request, null) as Enum;
                    strValue = eVal != null ? eVal.GetStringValue(eVal.ToString()) : null;
                }
                else
                {
                    var objVal = property.Property.GetValue(request, null);
                    strValue = objVal != null
                        ? (objVal is string ? (string) objVal : objVal.ToString())
                        : null;
                }

                if (!string.IsNullOrEmpty(strValue))
                {
                    var item = new Parameter
                    {
                        Name = property.Attr.ParameterName,
                        Type = property.Attr.ParameterType,
                        Value = strValue
                    };
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Gets body objects of a REST request.
        /// </summary>
        /// <typeparam name="T">The type of a retrieved data object.</typeparam>
        /// <param name="request">The Google Drive request.</param>
        /// <returns>REST request parameters.</returns>
        internal static IEnumerable<object> GetBodyObjects<T>(IDriveRequest<T> request) where T : IDriveData, new()
        {
            var attrType = typeof (RestBodyAttribute);
            var properties = request.GetType().GetProperties()
                .Where(p => p.IsDefined(attrType, false))
                .Select(p => new {Property = p, Attr = p.GetCustomAttributes(attrType, false).OfType<RestBodyAttribute>().FirstOrDefault()})
                .Where(x => x.Attr != null);

            return from propertyDef in properties
                let value = propertyDef.Property.GetValue(request, null)
                where value != null
                select value;
        }
    }
}