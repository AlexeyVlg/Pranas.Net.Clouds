using System;
using System.Linq;

namespace Net.Pranas.Client.GoogleDrive.Business.Meta.Ext
{
    /// <summary>
    /// Represents extensions enum vaues.
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// Gets a string value.
        /// </summary>
        /// <param name="eval">The enum value.</param>
        /// <param name="defValue">The default string value.</param>
        /// <returns>A string value.</returns>
        public static string GetStringValue(this Enum eval, string defValue = null)
        {
            var type = eval.GetType();
            var fi = type.GetField(eval.ToString());
            var attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false).OfType<StringValueAttribute>().ToArray();
            string result = attrs.Length > 0 ? attrs[0].Value : defValue;
            return result;
        }
    }
}
