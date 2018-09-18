using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web.Http;
using Matlab.Api.Tools;
using Matlab.DataModel;
using Matlab.Logger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

//using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace Matlab.Api.Controllers
{

    [Authorize]
    public class PackagesController : ApiController
    {
        public ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        MatlabDb db = new MatlabDb();

        [HttpPost]
        public async Task<ResponseMessage> Categories()
        {
            //string ip = StaticTools.GetIp(Request);
            try
            {
                var result = await db.Catergories.ToListAsync();
                LogThis.BaseLog(Request, LogLevel.Info, Actions.CategoriesRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Count,result.Count },
                    {LogProperties.Message,ErrorMessages.Successful }
                });
                return Tools.ResponseMessage.OkWithResult(result);
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.CategoriesRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Error,e }
                });
                return Tools.ResponseMessage.InternalError;
            }

        }


        [HttpPost]
        public async Task<ResponseMessage> Packages(IdRequestViewModel model)
        {
            string ip = StaticTools.GetIp(Request);
            try
            {
                var category = await db.Catergories.FindAsync(model.Id);
                if (category == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.PackagesOfCategoryRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id,model.Id},
                        {LogProperties.Message,ErrorMessages.RequestedCategoryNotFound }
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.RequestedCategoryNotFound);
                }

                var result = category.Packages.Select(x => new PackageMinimalViewModel(x)).ToList();
                LogThis.BaseLog(Request, LogLevel.Info, Actions.PackagesOfCategoryRequested,
                    new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.Id},
                        {LogProperties.Count, result.Count},
                        {LogProperties.Message, ErrorMessages.Successful}

                    });
                return Tools.ResponseMessage.OkWithResult(result);
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.CategoriesRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Error,e }
                });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> MyPackages()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);
                var userPackages = user.UserPackages;
                LogThis.BaseLog(Request, LogLevel.Info, Actions.PackagesOfUserRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Count, userPackages?.Count},
                    {LogProperties.Message,ErrorMessages.Successful}
                });
                return Tools.ResponseMessage.OkWithResult(userPackages?.Select(x => new UserPackageMinimalViewModel(x)));
            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.CategoriesRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Error,e }
                });
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> UserPackageBoxes(IdRequestViewModel model)
        {
            try
            {
                var package = await db.Packages.FindAsync(model.Id);
                if (package == null)
                {
                    LogThis.BaseLog(Request, LogLevel.Warn, Actions.PackageBoxesRequested, new Dictionary<LogProperties, object>
                    {
                        {LogProperties.Id, model.Id},
                        {LogProperties.Message,ErrorMessages.PackageNotFound }
                    });
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, ErrorMessages.PackageNotFound);
                }

                var userId = User.Identity.GetUserId();
                var packageBoxes = package.Boxes.Select(x=>new UserBoxMinimalViewModel(x,userId)).ToList();
                LogThis.BaseLog(Request, LogLevel.Info, Actions.PackageBoxesRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Id, model.Id},
                    {LogProperties.Message,ErrorMessages.Successful },
                    {LogProperties.Count,packageBoxes.Count }
                });
                return Tools.ResponseMessage.OkWithResult(packageBoxes);


            }
            catch (Exception e)
            {
                LogThis.BaseLog(Request, LogLevel.Error, Actions.CategoriesRequested, new Dictionary<LogProperties, object>
                {
                    {LogProperties.Error,e }
                });
                return Tools.ResponseMessage.InternalError;
            }
        }


    }
}
