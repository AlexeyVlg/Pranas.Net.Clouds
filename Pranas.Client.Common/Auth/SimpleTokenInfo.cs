using System;
using System.Runtime.Serialization;

namespace Net.Pranas.Client.Common.Auth
{
    [DataContract]
    public class SimpleTokenInfo : ITokenInfo
    {
        [DataMember]
        public string AccessToken { get; set; }

        [DataMember]
        public string TokenType { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime? ExpiresAt { get; set; }

        [DataMember]
        public string RefreshToken { get; set; }
    }
}
