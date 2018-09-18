using Matlab.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Matlab.Api.Tools;
using Matlab.Logger;
using Microsoft.AspNet.Identity.Owin;

namespace Matlab.Api.Controllers
{
    [Authorize]
    public class LearningController : ApiController
    {

        public ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        MatlabDb db = new MatlabDb();

        [HttpPost]
        public async Task<ResponseMessage> LearnedArticle(LearningViewModel model)
        {
            try
            {
                var userPackageBox = await db.UserPackageBoxes.FindAsync(model.UserBoxId);

                if (userPackageBox == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.LearnedArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.UserBoxId},
                            {LogProperties.IdType, typeof(UserPackageBox)},
                            {LogProperties.Message,ErrorMessages.UserPackageBoxNotFound },
                            {LogProperties.AdditionalData,model.Order }
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.UserPackageBoxNotFound);
                }

                if (userPackageBox.State == BoxState.NotOwned)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.LearnedArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.UserBoxId},
                            {LogProperties.IdType, typeof(UserPackageBox)},
                            {LogProperties.Message,ErrorMessages.UserNotOwnedBox },
                            {LogProperties.AdditionalData,model.Order },
                            {LogProperties.State,BoxState.NotOwned }
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.UserStatusFaild, ErrorMessages.UserNotOwnedBox);
                }

                var articles = userPackageBox.Box.Articles.ToList();
                var article = articles.FirstOrDefault(x => x.Order == model.Order);
                if (article == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.LearnedArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.UserBoxId},
                            {LogProperties.IdType, typeof(UserPackageBox)},
                            {LogProperties.Message,ErrorMessages.ArticleNotFound },
                            {LogProperties.AdditionalData,model.Order }
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.ArticleNotFound);
                }
                var articleIndex = articles.IndexOf(article) + 1;
                if (userPackageBox.State != BoxState.Learning || articleIndex <= userPackageBox.StateValue)
                {
                    LogThis.BaseLog(Request, LogLevel.Info, Actions.LearnedArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.UserBoxId},
                            {LogProperties.IdType, typeof(UserPackageBox)},
                            {LogProperties.Message, ErrorMessages.ArticleLearnRepeated},
                            {LogProperties.AdditionalData, model.Order},
                            {LogProperties.State, userPackageBox.State}
                        });
                    return Tools.ResponseMessage.OkWithMessage(ErrorMessages.ArticleLearnRepeated);
                }

                LogThis.BaseLog(Request, LogLevel.Info, Actions.ChangeBoxStateValue,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.UserBoxId},
                        {LogProperties.IdType, typeof(UserPackageBox)},
                        {LogProperties.Message, ErrorMessages.ArticleLearned},
                        {LogProperties.AdditionalData, model.Order},
                        {LogProperties.State, userPackageBox.State},
                        {LogProperties.From, userPackageBox.StateValue},
                        {LogProperties.To, articleIndex}
                    });
                userPackageBox.StateValue++;
                if (userPackageBox.Box.Articles.Count==userPackageBox.StateValue)
                {
                    LogThis.BaseLog(Request, LogLevel.Info, Actions.ChangeBoxState, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.UserBoxId},
                        {LogProperties.IdType, typeof(UserPackageBox)},
                        {LogProperties.Message, ErrorMessages.UserBoxStateChanged},
                        {LogProperties.AdditionalData, model.Order},
                        {LogProperties.From, BoxState.Learning},
                        {LogProperties.To, BoxState.Examing}
                    });
                    userPackageBox.State = BoxState.Examing;
                    LogThis.BaseLog(Request, LogLevel.Info, Actions.ChangeBoxStateValue,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.UserBoxId},
                            {LogProperties.IdType, typeof(UserPackageBox)},
                            {LogProperties.Message, ErrorMessages.UserBoxStateValueChange},
                            {LogProperties.AdditionalData, model.Order},
                            {LogProperties.State, userPackageBox.State},
                            {LogProperties.From, userPackageBox.StateValue},
                            {LogProperties.To, 0}
                        });
                    userPackageBox.StateValue = 0;
                    return Tools.ResponseMessage.OkWithMessage(ErrorMessages.UserBoxStateChanged);
                }
                return Tools.ResponseMessage.OkWithMessage(ErrorMessages.ArticleLearned);
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
    }
}
