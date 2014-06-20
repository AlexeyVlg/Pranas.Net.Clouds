using Net.Pranas.Client.GoogleDrive.Business.Model;
using RestSharp;
using RestSharp.Deserializers;
using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Interaction
{
    /// <summary>
    /// Represents a Google Drive response.
    /// </summary>
    public class DriveResponse<T> where T : IDriveData
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a Google Drive response.
        /// </summary>
        /// <param name="data">The responsed data.</param>
        public DriveResponse(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Constructs a Google Drive response.
        /// </summary>
        /// <param name="restResponse">The REST response.</param>
        internal DriveResponse(IRestResponse<T> restResponse)
            :this(restResponse.Data)
        {
        }

        /// <summary>
        /// Constructs a Google Drive response.
        /// </summary>
        /// <param name="restResponse">The REST response.</param>
        internal DriveResponse(IRestResponse restResponse)
        {
            if (!string.IsNullOrEmpty(restResponse.Content))
            {
                var deserializer = new JsonDeserializer();
                Data = deserializer.Deserialize<T>(restResponse);
            }
        }

        /// <summary>
        /// Constructs a Google Drive response.
        /// </summary>
        /// <param name="restResponse">The REST response.</param>
        /// <param name="postInitialization">The action to post-initialize a responsed data.</param>
        internal DriveResponse(IRestResponse<T> restResponse, Action<T> postInitialization)
            :this(restResponse)
        {
            postInitialization(Data);
        }

        /// <summary>
        /// Constructs a Google Drive response.
        /// </summary>
        /// <param name="restResponse">The REST response.</param>
        /// <param name="postInitialization">The action to post-initialize a responsed data.</param>
        internal DriveResponse(IRestResponse restResponse, Action<T> postInitialization)
            :this(restResponse)
        {
            postInitialization(Data);
        }

        #endregion

        /// <summary>
        /// Gets the responsed data.
        /// </summary>
        public T Data { get; private set; }
    }
}
