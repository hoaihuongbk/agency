using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;

namespace Sima.Common.Model.Types
{
    public class CreateUserDataReponse : IMeta
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string UserName { get; set; }
        public string ReferrerUrl { get; set; }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public Dictionary<string, string> Meta { get; set; }
    }
}