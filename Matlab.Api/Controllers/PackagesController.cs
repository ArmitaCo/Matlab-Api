using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Matlab.Api.Tools;
using Matlab.DataModel;
using Microsoft.AspNet.Identity;
//using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace Matlab.Api.Controllers
{
    public class PackagesController : ApiController
    {
        MatlabDb db = new MatlabDb();
        // GET: api/Packages
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Packages/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Packages
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Packages/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Packages/5
        //public void Delete(int id)
        //{
        //}
        //[Route("Packages/Categories")]
        [HttpPost]
        public async Task<ResponseMessage> Categories()
        {
            string ip = StaticTools.GetIp(Request);
            try
            {
                var result = await db.Catergories.ToListAsync();
                return Tools.ResponseMessage.OkWithResult(result);
            }
            catch (Exception)
            {
                return Tools.ResponseMessage.InternalError;
            }
        }


        [HttpPost]
        public async Task<ResponseMessage> Packages([FromBody]int id)
        {
            string ip = StaticTools.GetIp(Request);
            try
            {
                var category = await db.Catergories.FindAsync(id);
                if (category == null)
                {
                    return new ResponseMessage(Tools.ResponseMessage.ResponseCode.NotFound, "دسته بندی مورد نظر یافت نشد");
                }

                var result = category.Packages.ToList();
                return Tools.ResponseMessage.OkWithResult(result);
            }
            catch (Exception)
            {
                return Tools.ResponseMessage.InternalError;
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> MyPackages()
        {
            string ip = StaticTools.GetIp(Request);
            try
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                var userPackages = user.UserPackages;
                return Tools.ResponseMessage.OkWithResult(userPackages);
            }
            catch (Exception)
            {
                return Tools.ResponseMessage.InternalError;
            }
        }
    }
}
