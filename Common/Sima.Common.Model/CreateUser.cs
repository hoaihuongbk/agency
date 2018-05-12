using ServiceStack;
using Sima.Common.Model.Types;

namespace Sima.Common.Model
{
    [Route("/user/create", "POST")]
    public class CreateUser : IReturn<CreateUserReponse>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? AutoLogin { get; set; }
        public string Continue { get; set; }
        public string Role { get; set; }
    }
}