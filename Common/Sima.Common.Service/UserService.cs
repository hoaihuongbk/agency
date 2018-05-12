using System;
using System.Collections.Generic;
using System.Globalization;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;
using Sima.Common.Constant;
using Sima.Common.Model;
using Sima.Common.Model.Types;

namespace Sima.Common.Service
{
    public class UserService : BaseService
    {
        private IAuthRepository AuthRepo { get; }
        private IAuthEvents AuthEvents { get; }
        public UserService(IAuthRepository authRepo, IAuthEvents authEvents)
        {
            AuthRepo = authRepo;
            AuthEvents = authEvents;
        }
        
        public object Post(CreateUser request)
        {
            if (HostContext.GetPlugin<AuthFeature>()?.SaveUserNamesInLowerCase == true)
            {
                if (request.UserName != null)
                    request.UserName = request.UserName.ToLower();
                if (request.Email != null)
                    request.Email = request.Email.ToLower();
            }
            
            CreateUserDataReponse response = null;
            
            var newUserAuth = ToUserAuth(AuthRepo, request);
            var user = AuthRepo.CreateUserAuth(newUserAuth, request.Password);

            if (!request.Role.IsNullOrEmpty())
            {
                using (var assignRoleService = ResolveService<AssignRolesService>())
                {
                    var assignRoleResponse = assignRoleService.Post(new AssignRoles()
                    {
                        UserName = user.UserName,
                        Roles = new List<string>() {request.Role }
                    });
                    switch (assignRoleResponse)
                    {
                        case IHttpError _:
                            throw (Exception)assignRoleResponse;
                       default:
                            break;
                    }
                }
            }
            
            if (request.AutoLogin.GetValueOrDefault())
            {
                using (var authService = ResolveService<AuthenticateService>())
                {
                    var authResponse = authService.Post(
                        new Authenticate {
                            provider = CredentialsAuthProvider.Name,
                            UserName = request.UserName ?? request.Email,
                            Password = request.Password,
                            Continue = request.Continue
                        });

                    switch (authResponse)
                    {
                        case IHttpError _:
                            throw (Exception)authResponse;
                        case AuthenticateResponse typedResponse:
                            response = new CreateUserDataReponse
                            {
                                SessionId = typedResponse.SessionId,
                                UserName = typedResponse.UserName,
                                ReferrerUrl = typedResponse.ReferrerUrl,
                                UserId = user.Id.ToString(CultureInfo.InvariantCulture),
                                BearerToken = typedResponse.BearerToken,
                                RefreshToken = typedResponse.RefreshToken,
                            };
                            break;
                    }
                }
            }

            var session = GetSession();
            if (!request.AutoLogin.GetValueOrDefault())
                session.PopulateSession(user, new List<IAuthTokens>());

            session.OnRegistered(Request, session, this);
            
            AuthEvents?.OnRegistered(Request, session, this);
            if (response == null)
            {
                response = new CreateUserDataReponse
                {
                    UserId = user.Id.ToString(CultureInfo.InvariantCulture),
                    ReferrerUrl = request.Continue,
                    UserName = session.UserName,
                };
            }

            var isHtml = Request.ResponseContentType.MatchesContentType(MimeTypes.Html);
            if (!isHtml)
                return new CreateUserReponse()
                {
                    Status = (int) CommonStatus.Success,
                    Data = response
                };
            if (string.IsNullOrEmpty(request.Continue))
                return response;

            return new HttpResult(response)
            {
                Location = request.Continue
            };

        }

        private static IUserAuth ToUserAuth(IAuthRepository authRepo, CreateUser request)
        {
            var to = authRepo is ICustomUserAuth customUserAuth
                ? customUserAuth.CreateUserAuth()
                : new UserAuth();

            to.PopulateInstance(request);
            to.PrimaryEmail = request.Email;
            return to;
        }
    }
}