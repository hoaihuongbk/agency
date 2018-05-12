using ServiceStack;
using ServiceStack.Configuration;

namespace Sima.Common.Model
{
    public abstract class BaseRequest : IHasVersion
    {
        [ApiMember(Description = "Version. Ex: 1,2,3,...")]
        public int Version { get; set; }
        //public string Culture { get; set; }
        protected BaseRequest()
        {
            Version = ConfigUtils.GetAppSetting("AppVersion", 1);
            //Culture = System.Globalization.CultureInfo.CurrentCulture.Name; //Default culture
        }
    }

    //[ProtoContract]
}