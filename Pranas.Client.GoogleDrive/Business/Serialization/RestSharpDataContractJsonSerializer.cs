using RestSharp.Serializers;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Net.Pranas.Client.GoogleDrive.Business.Serialization
{
    /// <summary>
    /// Represents a JSON serializer with DataContract attribute.
    /// </summary>
    public class RestSharpDataContractJsonSerializer : ISerializer
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a JSON serializer.
        /// </summary>
        public RestSharpDataContractJsonSerializer()
        {
            ContentType = DefaultContentType;
        }

        #endregion

        public string Serialize(object obj)
        {
            string result;

            if (obj == null)
            {
                result = string.Empty;
            }
            else
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                byte[] jsonData;

                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, obj);
                    jsonData = stream.ToArray();
                }

                result = Encoding.UTF8.GetString(jsonData);
            }

            return result;
        }

        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string DateFormat { get; set; }

        public string ContentType { get; set; }

        private const string DefaultContentType = "application/json";
    }
}
