using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents metadata about an image media.
    /// </summary>
    [DataContract]
    public class DriveImageMediaMetadataInfo
    {
        /// <summary>
        /// Gets or sets the aperture used to create the photo (f-number).
        /// </summary>
        [DataMember(Name = "aperture")]
        public float Aperture { get; set; }

        /// <summary>
        /// Gets or sets the make of the camera used to create the photo.
        /// </summary>
        [DataMember(Name = "cameraMake")]
        public string CameraMake { get; set; }

        /// <summary>
        /// Gets or sets the model of the camera used to create the photo.
        /// </summary>
        [DataMember(Name = "cameraModel")]
        public string CameraModel { get; set; }

        /// <summary>
        /// Gets or sets the color space of the photo.
        /// </summary>
        [DataMember(Name = "colorSpace")]
        public string ColorSpace { get; set; }

        /// <summary>
        /// Gets or sets the date and time the photo was taken (EXIF format timestamp).
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the exposure bias of the photo (APEX value).
        /// </summary>
        [DataMember(Name = "exposureBias")]
        public float ExposureBias { get; set; }

        /// <summary>
        /// Gets or sets the exposure mode used to create the photo.
        /// </summary>
        [DataMember(Name = "exposureMode")]
        public string ExposureMode { get; set; }

        /// <summary>
        /// Gets or sets the length of the exposure, in seconds.
        /// </summary>
        [DataMember(Name = "exposureTime")]
        public float ExposureTime { get; set; }

        /// <summary>
        /// Gets or sets the whether a flash was used to create the photo.
        /// </summary>
        [DataMember(Name = "flashUsed")]
        public bool FlashUsed { get; set; }

        /// <summary>
        /// Gets or sets the focal length used to create the photo, in millimeters.
        /// </summary>
        [DataMember(Name = "focalLength")]
        public float FocalLength { get; set; }

        /// <summary>
        /// Gets or sets the height of the image in pixels.
        /// </summary>
        [DataMember(Name = "height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the ISO speed used to create the photo.
        /// </summary>
        [DataMember(Name = "isoSpeed")]
        public int IsoSpeed { get; set; }

        /// <summary>
        /// Gets or sets the lens used to create the photo.
        /// </summary>
        [DataMember(Name = "lens")]
        public string Lens { get; set; }

        /// <summary>
        /// Gets or sets the geographic location information stored in the image.
        /// </summary>
        [DataMember(Name = "location")]
        public DriveLocationInfo Location { get; set; }

        /// <summary>
        /// Gets or sets the smallest f-number of the lens at the focal length used to create the photo (APEX value).
        /// </summary>
        [DataMember(Name = "maxApertureValue")]
        public float MaxApertureValue { get; set; }

        /// <summary>
        /// Gets or sets the metering mode used to create the photo.
        /// </summary>
        [DataMember(Name = "meteringMode")]
        public string MeteringMode { get; set; }

        /// <summary>
        /// Gets or sets the rotation in clockwise degrees from the image's original orientation.
        /// </summary>
        [DataMember(Name = "rotation")]
        public int Rotation { get; set; }

        /// <summary>
        /// Gets or sets the type of sensor used to create the photo.
        /// </summary>
        [DataMember(Name = "sensor")]
        public string Sensor { get; set; }

        /// <summary>
        /// Gets or sets the distance to the subject of the photo, in meters.
        /// </summary>
        [DataMember(Name = "subjectDistance")]
        public int SubjectDistance { get; set; }

        /// <summary>
        /// Gets or sets the white balance mode used to create the photo.
        /// </summary>
        [DataMember(Name = "whiteBalance")]
        public string WhiteBalance { get; set; }

        /// <summary>
        /// Gets or sets the width of the image in pixels.
        /// </summary>
        [DataMember(Name = "width")]
        public int Width { get; set; }
    }
}
