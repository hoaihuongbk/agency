using ServiceStack;
using Sima.Common.Constant;
using Sima.Common.Model;
using Sima.Common.Model.Types;
using Xunit;

namespace Agency.SmokeTests
{
    public class UserTests : CommonTests
    {
        [Fact]
        public void CreateUserTest()
        {
            using (var client = new JsonServiceClient(BaseUrl))
            {
                var result = client.Post<CreateUserReponse>(USER_CREATE, new CreateUser()
                {
                    UserName = "hoaihuongbk",
                    FirstName = "Hương",
                    LastName = "Hoài",
                    DisplayName = "Hoài Hương",
                    Email = "hoaihuongvuonghuynh@gmail.com",
                    Password = "123456",
                    Role = RoleType.Admin
                });
                Assert.Contains(result.Status, new[]{ (int) CommonStatus.Success, (int) CommonStatus.UndefinedError });
            }
           
        }
    }
}