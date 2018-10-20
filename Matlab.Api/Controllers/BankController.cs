using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Matlab.DataModel;
using Microsoft.AspNet.Identity;

namespace Matlab.Api.Controllers
{
    //[Authorize]
    //public class BankController : Controller
    //{
    //    MatlabDb db = new MatlabDb();
    //    public async Task<ActionResult> BuyPackage(IdRequestViewModel packageId)
    //    {
    //        var package = await db.Packages.FindAsync(packageId.Id);
    //        if (package == null)
    //        { //log this
    //            return HttpNotFound();
    //        }

    //        var userId = User.Identity.GetUserId();

    //        var userPackage = package.UserPackages.FirstOrDefault(x => x.UserId == userId);

    //        if (userPackage!=null)
    //        {
    //            //log this
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"کاربر قبلا این بسته را خریداری کرده است");
    //        }



    //    }
    //}
}