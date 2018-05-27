using Agency.ServiceModel;
using ServiceStack;
using Sima.Common.Constant;
using Xunit;

namespace Agency.SmokeTests
{
    public class SettingTests : CommonTests
    {
        [Fact]
        public void GetSettingTest()
        {
            using (var client = new JsonServiceClient(BaseUrl))
            {
                var result = client.Get<SettingResponse>(SETTING_GET);
                Assert.Equal((int) CommonStatus.Success, result.Status);
            }
           
        }
    }
}