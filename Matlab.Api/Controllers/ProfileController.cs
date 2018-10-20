using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Matlab.Api.Tools;
using Matlab.DataModel;
using Matlab.Logger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Matlab.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ProfileController : ApiController
    {
        //public ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private readonly MatlabDb _db = new MatlabDb();

        [HttpPost]
        public async Task<ResponseMessage> ProfileImagesList()
        {
            //log this
            return Tools.ResponseMessage.OkWithResult(await _db.AvatarImages.ToListAsync());
        }

        [HttpPost]
        public ResponseMessage ProfileData()
        {
            var userId = User.Identity.GetUserId();
            var user = _db.Users.Find(userId);
            LogThis.BaseLog(Request, LogLevel.Info, Actions.ProfileDataRequested,
                new Dictionary<LogProperties, object>
                {
                    {LogProperties.Id, userId},
                    {LogProperties.IdType,typeof(ApplicationUser) },
                    {LogProperties.Message, ErrorMessages.Successful}
                });
            return Tools.ResponseMessage.OkWithResult(new ProfileViewModel(user));
        }

        [HttpPost]
        public async Task<ResponseMessage> SetAvatar(IdRequestViewModel avatarId)
        {
            var avatar = await _db.AvatarImages.FindAsync(avatarId.Id);
            if (avatar == null)
            {
                LogThis.BaseLog(Request, LogLevel.Info, Actions.ProfileDataRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, avatarId.Id},
                        {LogProperties.IdType,typeof(AvatarImage) },
                        {LogProperties.Message, ErrorMessages.AvatarImageNotFound}
                    });
                return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.AvatarImageNotFound);
            }

            var user = _db.Users.Find(User.Identity.GetUserId());
            user.AvatarImage = avatar;
            await _db.SaveChangesAsync();
            LogThis.BaseLog(Request, LogLevel.Info, Actions.ProfileDataRequested,
                new Dictionary<LogProperties, object>
                {
                    {LogProperties.Id, avatarId.Id},
                    {LogProperties.IdType,typeof(AvatarImage) },
                    {LogProperties.Message, ErrorMessages.Successful}
                });
            return Tools.ResponseMessage.Ok;
        }
    }
}
