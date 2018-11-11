using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Matlab.Api.Tools;
using Matlab.DataModel;
using Matlab.Logger;
using Microsoft.AspNet.Identity;

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
            try
            {
                var avatarImages = await _db.AvatarImages.ToListAsync();
                LogThis.BaseLog(Request, LogLevel.Info, Actions.ProfileImageListRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Count, avatarImages.Count},
                        {LogProperties.Message, ErrorMessages.Successful}
                    });
                return Tools.ResponseMessage.OkWithResult(avatarImages);
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.ProfileImageListRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Error, e}
                    });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public ResponseMessage ProfileData()
        {
            try
            {

                var userId = User.Identity.GetUserId();
                var user = _db.Users.Find(userId);
                LogThis.BaseLog(Request, LogLevel.Info, Actions.ProfileDataRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, userId},
                        {LogProperties.IdType, typeof(ApplicationUser)},
                        {LogProperties.Message, ErrorMessages.Successful}
                    });
                return Tools.ResponseMessage.OkWithResult(new ProfileViewModel(user));
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.ProfileDataRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Error, e}
                    });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> SetAvatar(IdRequestViewModel avatarId)
        {
            try
            {
                var avatar = await _db.AvatarImages.FindAsync(avatarId.Id);
                if (avatar == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Info, Actions.SetAvatarRequested,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, avatarId.Id},
                            {LogProperties.IdType, typeof(AvatarImage)},
                            {LogProperties.Message, ErrorMessages.AvatarImageNotFound}
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound,
                        ErrorMessages.AvatarImageNotFound);
                }

                var user = _db.Users.Find(User.Identity.GetUserId());
                user.AvatarImage = avatar;
                await _db.SaveChangesAsync();
                LogThis.BaseLog(Request, LogLevel.Info, Actions.SetAvatarRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, avatarId.Id},
                        {LogProperties.IdType, typeof(AvatarImage)},
                        {LogProperties.Message, ErrorMessages.Successful}
                    });
                return Tools.ResponseMessage.Ok;
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.SetAvatarRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Error, e}
                    });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> LeaderBoard()
        {
            try
            {
                const int topN = 10;
                var user = _db.Users.Find(User.Identity.GetUserId());
                var userOrder = await _db.Users.CountAsync(x => x.Score > user.Score);
                ScoreBoardItemViewModel userItem = null;
                if (userOrder >= topN) //user is not in topN
                {
                    userItem = new ScoreBoardItemViewModel
                    {
                        Score = user.Score,
                        Name = user.UserName,
                        Order = userOrder
                    };
                }

                var y = await _db.Users.OrderByDescending(x => x.Score).Take(topN).ToListAsync().ConfigureAwait(false);
                var items = y.Select((x, i) =>
                    new ScoreBoardItemViewModel { Score = x.Score, Name = x.UserName, Order = i + 1 }).ToList();
                var challenge = await _db.Challenges.OrderByDescending(x => x.Finish).FirstOrDefaultAsync();
                var result = new ScoreBoardViewModel
                {
                    Items = items,
                    UserItem = userItem,
                    Challenge = challenge
                };
                LogThis.BaseLog(Request, LogLevel.Info, Actions.LeaderBoardRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.AdditionalData, $"رتبه کاربر {userOrder} میباشد"},
                        {LogProperties.Id, challenge?.Id},
                        {LogProperties.IdType, challenge?.GetType()},
                        {LogProperties.Count, items.Count},
                        {LogProperties.Message, ErrorMessages.Successful}
                    });
                return Tools.ResponseMessage.OkWithResult(result);

            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.LeaderBoardRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Error, e}
                    });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> BugReport(BugReportViewModel bug)
        {
            try
            {
                LogThis.BaseLog(Request, LogLevel.Info, Actions.BugReported,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.AdditionalData, bug.Bug},
                        {LogProperties.Message, ErrorMessages.Successful}
                    });
                _db.BugReports.Add(new BugReport
                {
                    DateTime = DateTime.Now,
                    UserId = User.Identity.GetUserId(),
                    Text = bug.Bug
                });
                await _db.SaveChangesAsync();
                return Tools.ResponseMessage.Ok;
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.BugReported,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Error, e}
                    });
                return Tools.ResponseMessage.InternalError;
            }
        }
    }
}
