using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a geographic location information.
    /// </summary>
    [DataContract]
    public class DriveLocationInfo
    {
        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        [DataMember(Name = "altitude")]
        public double Altitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }
    }
}
