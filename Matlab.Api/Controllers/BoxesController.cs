using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Authorize]
    public class BoxesController : ApiController
    {
        public ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        MatlabDb db = new MatlabDb();

        [HttpPost]
        public async Task<ResponseMessage> UserBoxQuestions(IdRequestViewModel model)
        {//id userpackagebox
            try
            {
                var userPackageBox = await db.UserPackageBoxes.FindAsync(model.Id);
                if (userPackageBox == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.UserPackageBoxQuestionsRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.Id},
                        {LogProperties.Message,ErrorMessages.UserPackageBoxNotFound }
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.UserPackageBoxNotFound);
                }

                switch (userPackageBox.State)
                {
                    case BoxState.NotOwned:
                        LogThis.BaseLog(Request, LogLevel.Info, Actions.UserPackageBoxQuestionsRequested,
                            new Dictionary<LogProperties, object>
                            {
                                {LogProperties.Id, model.Id},
                                {LogProperties.Message, ErrorMessages.UserNotOwnedBox},
                            });
                        return new ResponseMessage(Tools.ResponseMessage.ResponseCode.UserStatusFaild, ErrorMessages.UserNotOwnedBox);
                    case BoxState.Learning:
                        LogThis.BaseLog(Request, LogLevel.Info, Actions.UserPackageBoxQuestionsRequested, new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.Id},
                            {LogProperties.Message,ErrorMessages.UserPackageBoxNotLearned }
                        });
                        return new ResponseMessage(Tools.ResponseMessage.ResponseCode.UserStatusFaild, ErrorMessages.UserPackageBoxNotLearned);
                    default:

                        LogThis.BaseLog(Request, LogLevel.Info, Actions.UserPackageBoxQuestionsRequested,
                            new Dictionary<LogProperties, object>
                            {
                                {LogProperties.Id, model.Id},
                                {LogProperties.Message, ErrorMessages.Successful},
                                {LogProperties.Count, userPackageBox.Box.Articles.Count}
                            });
                        return Tools.ResponseMessage.OkWithResult(userPackageBox.Box.Articles.SelectMany(x => x.Questions).Select(x => new QuestionMinimalViewModel(x, User.Identity.GetUserId())));
                }
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.UserPackageBoxQuestionsRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Error,e }
                });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> UserBoxArticles(IdRequestViewModel model)
        {//id userpackagebox
            try
            {
                var userPackageBox = await db.UserPackageBoxes.FindAsync(model.Id);
                if (userPackageBox == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.UserPackageBoxArticlesRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.Id},
                        {LogProperties.Message,ErrorMessages.UserPackageBoxNotFound }
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.UserPackageBoxNotFound);
                }

                switch (userPackageBox.State)
                {
                    case BoxState.NotOwned:
                        LogThis.BaseLog(Request, LogLevel.Info, Actions.UserPackageBoxArticlesRequested,
                            new Dictionary<LogProperties, object>
                            {
                                {LogProperties.Id, model.Id},
                                {LogProperties.Message, ErrorMessages.UserNotOwnedBox},
                            });
                        return new ResponseMessage(Tools.ResponseMessage.ResponseCode.UserStatusFaild, ErrorMessages.UserNotOwnedBox);
                    default:

                        LogThis.BaseLog(Request, LogLevel.Info, Actions.UserPackageBoxArticlesRequested,
                            new Dictionary<LogProperties, object>
                            {
                                {LogProperties.Id, model.Id},
                                {LogProperties.Message, ErrorMessages.Successful},
                                {LogProperties.Count, userPackageBox.Box.Articles.Count}
                            });
                        return Tools.ResponseMessage.OkWithResult(userPackageBox.Box.Articles.Select(x => new ArticleMinimalViewModel(x)));
                }
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.UserPackageBoxArticlesRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Error,e }
                });
                return Tools.ResponseMessage.InternalError;
            }
        }
    }
}
