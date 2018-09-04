using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Matlab.Api.Tools;
using Matlab.DataModel;

namespace Matlab.Api.Controllers
{

    public class ArticlesController : ApiController
    {
        MatlabDb db = new MatlabDb();

        [HttpPost]
        public async Task<HttpResponseMessage> AddImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        string fileName = Guid.NewGuid()+ext;
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {



                            var filePath = HttpContext.Current.Server.MapPath("~/Images/Articles/" + fileName);

                            postedFile.SaveAs(filePath);

                            var articleId= int.Parse(httpRequest.Params["id"]);


                            var article = db.Articles.Find(articleId);
                            Debug.Assert(article != null, nameof(article) + " != null");
                            article.ImageSuggests.Add(new ImageSuggest()
                            {
                                ImageUrl = fileName 
                            });
                            await db.SaveChangesAsync();

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        [HttpPost]
        public async Task<ResponseMessage> SuggestedImageList(IdRequestViewModel model)
        {
            var article = await db.Articles.FindAsync(model.Id);
            Debug.Assert(article != null, nameof(article) + " != null");
            return Tools.ResponseMessage.OkWithResult(article.ImageSuggests.Select(x=>x.AbsoluteImageUrl).ToList());
        }


    }
}
