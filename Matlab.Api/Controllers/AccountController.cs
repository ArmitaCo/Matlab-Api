using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Matlab.Api.Providers;
using Matlab.Api.Results;
using Matlab.Api.Tools;
using Matlab.DataModel;
using Matlab.Logger;

namespace Matlab.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        MatlabDb db = new MatlabDb();
        public AccountController()
        {
        }

        

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public ResponseMessage GetUserInfo()
        {
            string ip = StaticTools.GetIp(Request);
            try
            {
                ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

                return Tools.ResponseMessage.OkWithResult(new UserInfoViewModel
                {
                    Email = User.Identity.GetUserName(),
                    HasRegistered = externalLogin == null,
                    LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
                });
            }
            catch (Exception)
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public ResponseMessage Logout()
        {
            string ip = StaticTools.GetIp(Request);
            try
            {
                Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                return Tools.ResponseMessage.Ok;
            }
            catch (Exception)
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ResponseMessage> GetManageInfo(string returnUrl, bool generateState = false)
        {
            //string ip = StaticTools.GetIp(Request);
            try
            {
                IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user == null)
                {
                    return null;
                }

                List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

                foreach (IdentityUserLogin linkedAccount in user.Logins)
                {
                    logins.Add(new UserLoginInfoViewModel
                    {
                        LoginProvider = linkedAccount.LoginProvider,
                        ProviderKey = linkedAccount.ProviderKey
                    });
                }

                if (user.PasswordHash != null)
                {
                    logins.Add(new UserLoginInfoViewModel
                    {
                        LoginProvider = LocalLoginProvider,
                        ProviderKey = user.UserName,
                    });
                }

                return Tools.ResponseMessage.OkWithResult(new ManageInfoViewModel
                {
                    LocalLoginProvider = LocalLoginProvider,
                    Email = user.UserName,
                    Logins = logins,
                    ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
                });
            }
            catch (Exception)
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<ResponseMessage> ChangePassword(ChangePasswordBindingModel model)
        {
            //string ip = StaticTools.GetIp(Request);
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, ModelState);
                }

                IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                    model.NewPassword);

                if (!result.Succeeded)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, result);
                }

                return Tools.ResponseMessage.Ok;
            }
            catch
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<ResponseMessage> SetPassword(SetPasswordBindingModel model)
        {
            //string ip = StaticTools.GetIp(Request);
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, ModelState); ;
                }

                IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

                if (!result.Succeeded)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, result);
                }

                return Tools.ResponseMessage.Ok;
            }
            catch
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<ResponseMessage> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            //string ip = StaticTools.GetIp(Request);
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, ModelState); ;
                }

                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

                if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                    && ticket.Properties.ExpiresUtc.HasValue
                    && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, "ورود خارجی ناموفق"); ;

                }

                ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

                if (externalData == null)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, "The external login is already associated with an account.");
                }

                IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

                if (!result.Succeeded)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, result);
                }

                return Tools.ResponseMessage.Ok;
            }
            catch
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<ResponseMessage> RemoveLogin(RemoveLoginBindingModel model)
        {
            //string ip = StaticTools.GetIp(Request);
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, ModelState); ;
                }

                IdentityResult result;

                if (model.LoginProvider == LocalLoginProvider)
                {
                    result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
                }
                else
                {
                    result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                        new UserLoginInfo(model.LoginProvider, model.ProviderKey));
                }

                if (!result.Succeeded)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, result);
                }

                return Tools.ResponseMessage.Ok;
            }
            catch
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            //string ip = StaticTools.GetIp(Request);
            try
            {
                if (error != null)
                {
                    return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
                }

                if (!User.Identity.IsAuthenticated)
                {
                    return new ChallengeResult(provider, this);
                }

                ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

                if (externalLogin == null)
                {
                    return InternalServerError();
                }

                if (externalLogin.LoginProvider != provider)
                {
                    Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    return new ChallengeResult(provider, this);
                }

                ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                    externalLogin.ProviderKey));

                bool hasRegistered = user != null;

                if (hasRegistered)
                {
                    Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                       OAuthDefaults.AuthenticationType);
                    ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                        CookieAuthenticationDefaults.AuthenticationType);

                    AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                    Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
                }
                else
                {
                    IEnumerable<Claim> claims = externalLogin.GetClaims();
                    ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                    Authentication.SignIn(identity);
                }

                return Tools.ResponseMessage.Ok;
            }
            catch
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
            user.AddScore(ScoreReason.Register);
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new ResponseMessage(Tools.ResponseMessage.ResponseCode.AuthenticationFailed, ErrorMessages.RegisterError, result.Errors);
            }
            
            return Tools.ResponseMessage.Ok;
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
