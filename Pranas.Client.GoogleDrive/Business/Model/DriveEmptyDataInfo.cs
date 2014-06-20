using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Empty data.
    /// </summary>
    [DataContract]
    public class DriveEmptyDataInfo : IDriveData
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "errorDescription")]
        public string ErrorDescription { get; set; }
    }
}
