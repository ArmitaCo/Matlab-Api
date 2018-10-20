using Matlab.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Matlab.Api.Tools;
using Matlab.Logger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Matlab.Api.Controllers
{
    [Authorize]
    public class LearningController : ApiController
    {

        public ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        MatlabDb db = new MatlabDb();

        [HttpPost]
        public async Task<ResponseMessage> QuestionViewed(QuestionReviewingViewModel model)
        {
            try
            {
                var question = await db.Questions.FindAsync(model.QuestionId);
                if (question == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.QuestionViewdRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.QuestionId},
                        {LogProperties.IdType, typeof(Question)},
                        {LogProperties.Message, ErrorMessages.QuestionNotFound},
                        {LogProperties.AdditionalData,model.BoxId}
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.QuestionNotFound);
                }

                var box = await db.Boxes.FindAsync(model.BoxId);
                if (box == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.QuestionViewdRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.BoxId},
                        {LogProperties.IdType, typeof(Box)},
                        {LogProperties.Message, ErrorMessages.PackageBoxNotFound},
                        {LogProperties.AdditionalData,model.QuestionId }
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.PackageBoxNotFound);
                }

                var userPackageBox =
                    box.UserPackageBoxes.FirstOrDefault(x => x.UserPackage.UserId == User.Identity.GetUserId());

                if (userPackageBox == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.QuestionViewdRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.BoxId},
                        {LogProperties.IdType, typeof(Box)},
                        {LogProperties.Message, ErrorMessages.UserPackageBoxNotFound},
                        {LogProperties.AdditionalData,model.QuestionId }
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.UserPackageBoxNotFound);
                }

                if (userPackageBox.State != BoxState.Examing)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.QuestionViewdRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.BoxId},
                        {LogProperties.IdType, typeof(Box)},
                        {LogProperties.Message, ErrorMessages.UserPackageBoxStateIncorrect},
                        {LogProperties.AdditionalData,model.QuestionId }
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.UserPackageBoxStateIncorrect);
                }

                int index = box.Articles.ToList().IndexOf(question.Article);
                if (index == -1)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.QuestionViewdRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.BoxId},
                        {LogProperties.IdType, typeof(Box)},
                        {LogProperties.Message, ErrorMessages.QuestionNotInThisBox},
                        {LogProperties.AdditionalData,model.QuestionId }

                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.QuestionNotInThisBox);
                }
                LogThis.BaseLog(Request, LogLevel.Info, Actions.QuestionViewdRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Id, model.BoxId},
                    {LogProperties.IdType, typeof(Box)},
                    {LogProperties.Message, ErrorMessages.Successful},
                    {LogProperties.From,userPackageBox.StateValue },
                    {LogProperties.To,index+1 },
                    {LogProperties.AdditionalData,model.QuestionId }
                });
                userPackageBox.StateValue = index + 1;
                await db.SaveChangesAsync();
                return Tools.ResponseMessage.OkWithMessage(ErrorMessages.Successful);
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.UserPackageBoxQuestionsRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Error, e}
                    });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> AnsweringArticle(AnsweringViewModel model)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var answer = await db.Answers.FindAsync(model.AnswerId);
                if (answer == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.AnsweredArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.AnswerId},
                            {LogProperties.IdType, typeof(Answer)},
                            {LogProperties.Message, ErrorMessages.AnswerNotFound},
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound,
                        ErrorMessages.AnswerNotFound);
                }

                var userPackageBox =
                    answer.Question.Article.Box.UserPackageBoxes.FirstOrDefault(x => x.UserPackage.UserId == userId);
                if (userPackageBox == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.AnsweredArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.AnswerId},
                            {LogProperties.IdType, typeof(Answer)},
                            {LogProperties.Message, ErrorMessages.UserPackageBoxNotFound},
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound,
                        ErrorMessages.UserPackageBoxNotFound);
                }

                if (userPackageBox.State != BoxState.Examing)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.AnsweredArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.AnswerId},
                            {LogProperties.IdType, typeof(Answer)},
                            {LogProperties.Message, ErrorMessages.UserPackageBoxStateIncorrect},
                            {LogProperties.From, userPackageBox.State.ToString()},
                            {LogProperties.To, BoxState.Examing.ToString()}
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.UserStatusFaild,
                        ErrorMessages.UserPackageBoxStateIncorrect);
                }

                UserAnswer userAnswer = answer.Question.UserAnswers.FirstOrDefault(x => x.UserId == userId);

                if (userAnswer != null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.AnsweredArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.AnswerId},
                            {LogProperties.IdType, typeof(Answer)},
                            {LogProperties.Message, ErrorMessages.UserAnswerdBefore},
                        });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.Expire,
                        ErrorMessages.UserAnswerdBefore);
                }

                answer.Question.UserAnswers.Add(new UserAnswer()
                {
                    AnswerId = answer.Id,
                    DateTime = DateTime.Now,
                    UserId = userId
                });
                await db.SaveChangesAsync();

                if (userPackageBox.Box.Articles.SelectMany(x => x.Questions).Select(x => new QuestionMinimalViewModel(x, User.Identity.GetUserId())).All(x => x.UserAnswerId != null))
                {
                    LogThis.BaseLog(Request, LogLevel.Info, Actions.AnsweredArticle, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.AnswerId},
                        {LogProperties.IdType, typeof(Answer)},
                        {LogProperties.Message, ErrorMessages.UserBoxStateChanged},
                        {LogProperties.From, BoxState.Examing.ToString()},
                        {LogProperties.To, BoxState.Finished.ToString()}

                    });
                    LogThis.BaseLog(Request, LogLevel.Info, Actions.AnsweredArticle, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.AnswerId},
                        {LogProperties.IdType, typeof(Answer)},
                        {LogProperties.Message, ErrorMessages.UserBoxStateValueChange},
                        {LogProperties.From, userPackageBox.StateValue},
                        {LogProperties.To, 0}

                    });
                    userPackageBox.StateValue = 0;
                    userPackageBox.State = BoxState.Finished;
                    await db.SaveChangesAsync();
                }

                LogThis.BaseLog(Request, LogLevel.Info, Actions.AnsweredArticle, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Id, model.AnswerId},
                    {LogProperties.IdType, typeof(Answer)},
                    {LogProperties.Message, answer.IsCorrect ? ErrorMessages.CorrectAnswer : ErrorMessages.WrongAnswer},
                });

                return Tools.ResponseMessage.OkWithMessage(answer.IsCorrect
                    ? ErrorMessages.CorrectAnswer
                    : ErrorMessages.WrongAnswer);
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.UserPackageBoxQuestionsRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Error, e}
                    });
                return Tools.ResponseMessage.InternalError;
            }
        }


        [HttpPost]
        public async Task<ResponseMessage> LearnedArticle(LearningViewModel model)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var userPackageBox = await db.UserPackageBoxes.FirstOrDefaultAsync(x =>
                    x.BoxId == model.BoxId && x.UserPackage.UserId == userId);

                //var userPackageBox = await db.UserPackageBoxes.FindAsync(model.UserBoxId);

                if (userPackageBox == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.LearnedArticle,
                        new Dictionary<LogProperties, object>
                        {
                            {LogProperties.Id, model.BoxId},
                            {LogProperties.IdType, typeof(Box)},
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
                            {LogProperties.Id, userPackageBox.Id},
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
                            {LogProperties.Id, userPackageBox.Id},
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
                            {LogProperties.Id, userPackageBox.Id},
                            {LogProperties.IdType, typeof(UserPackageBox)},
                            {LogProperties.Message, ErrorMessages.ArticleLearnRepeated},
                            {LogProperties.AdditionalData, model.Order},
                            {LogProperties.State, userPackageBox.State}
                        });
                    return Tools.ResponseMessage.OkWithMessage(ErrorMessages.ArticleLearnRepeated); //todo: review this message is correct?
                }

                LogThis.BaseLog(Request, LogLevel.Info, Actions.ChangeBoxStateValue,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, userPackageBox.Id},
                        {LogProperties.IdType, typeof(UserPackageBox)},
                        {LogProperties.Message, ErrorMessages.ArticleLearned},
                        {LogProperties.AdditionalData, model.Order},
                        {LogProperties.State, userPackageBox.State},
                        {LogProperties.From, userPackageBox.StateValue},
                        {LogProperties.To, articleIndex}
                    });
                userPackageBox.StateValue++;

                if (userPackageBox.Box.Articles.Count == userPackageBox.StateValue)
                {
                    LogThis.BaseLog(Request, LogLevel.Info, Actions.ChangeBoxState, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, userPackageBox.Id},
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
                            {LogProperties.Id, userPackageBox.Id},
                            {LogProperties.IdType, typeof(UserPackageBox)},
                            {LogProperties.Message, ErrorMessages.UserBoxStateValueChange},
                            {LogProperties.AdditionalData, model.Order},
                            {LogProperties.State, userPackageBox.State},
                            {LogProperties.From, userPackageBox.StateValue},
                            {LogProperties.To, 0}
                        });
                    userPackageBox.StateValue = 0;
                    db.SaveChanges();
                    return Tools.ResponseMessage.OkWithMessage(ErrorMessages.UserBoxStateChanged);
                }

                db.SaveChanges();
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
