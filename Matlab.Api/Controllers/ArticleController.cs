using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Matlab.DataModel;

namespace Matlab.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        MatlabDb db = new MatlabDb();

        // GET: Article
        [HttpPost]
        public async Task<ActionResult> AddImage(IEnumerable<HttpPostedFileBase> files, int id)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {

                    int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                    var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    string fileName = Guid.NewGuid() + ext;
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {

                        //var message = string.Format("Please Upload image of type .jpg,.gif,.png.");


                        //return new HttpStatusCodeResult(HttpStatusCode.BadGateway,message);
                        continue;
                    }
                    else if (file.ContentLength > MaxContentLength)
                    {

                        //var message = string.Format("Please Upload a file upto 1 mb.");


                        //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, message);
                        continue;
                    }
                    else
                    {



                        var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Images/Articles/" + fileName);

                        file.SaveAs(filePath);

                        var articleId = id;


                        var article = db.Articles.Find(articleId);
                        Debug.Assert(article != null, nameof(article) + " != null");
                        article.ImageSuggests.Add(new ImageSuggest()
                        {
                            ImageUrl = fileName
                        });
                        await db.SaveChangesAsync();

                    }
                }


            }
            return RedirectToAction("AddImage");
            //var res = string.Format("Please Upload a image.");

            //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, res);

            //file.SaveAs(Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));

        }

        public async Task<ActionResult> AddImage(int? id)
        {
            if (id.HasValue)
            {
                return View(await db.Articles.FindAsync(id));
            }
            var article = await db.Articles.FirstAsync(x => x.ImageSuggests.Count == 0);
            return View(article);
        }

        public async Task<ActionResult> SetImage(int? id)
        {
            if (id.HasValue)
            {
                return View(await db.Articles.FindAsync(id));
            }
            var article = await db.Articles.FirstAsync(x => (x.ImageUrl == null || x.ImageUrl == "") && x.ImageSuggests.Count > 1);
            return View(article);
        }

        [HttpPost]
        public async Task<ActionResult> SetImage(TwoIdRequestViewModel model)
        {
            var article = await db.Articles.FindAsync(model.Id1);
            var image = await db.ImageSuggests.FindAsync(model.Id2);
            Debug.Assert(image != null, nameof(image) + " != null");
            Debug.Assert(article != null, nameof(article) + " != null");
            article.ImageUrl = image.ImageUrl;
            await db.SaveChangesAsync();
            return RedirectToAction("SetImage");
        }

        [HttpPost]
        public async Task<ActionResult> AddImageAndExternalLink(AddPhotoAndExternalLinksViewModel model)
        {
            var articleId = model.id;


            var article = db.Articles.Find(articleId);
            if (model.files != null)
            {

                foreach (var file in model.files)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        string fileName = Guid.NewGuid() + ext;
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            //var message = string.Format("Please Upload image of type .jpg,.gif,.png.");


                            //return new HttpStatusCodeResult(HttpStatusCode.BadGateway,message);
                            continue;
                        }
                        else if (file.ContentLength > MaxContentLength)
                        {

                            //var message = string.Format("Please Upload a file upto 1 mb.");


                            //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, message);
                            continue;
                        }
                        else
                        {



                            var filePath =
                                System.Web.HttpContext.Current.Server.MapPath("~/Images/Articles/" + fileName);

                            file.SaveAs(filePath);


                            Debug.Assert(article != null, nameof(article) + " != null");
                            article.ImageSuggests.Add(new ImageSuggest()
                            {
                                ImageUrl = fileName
                            });
                            await db.SaveChangesAsync();

                        }
                    }


                }
            }

            if (model.links != null)
            {

                for (int i = 0; i < model.links.Count(); i++)
                {
                    var link = model.links[i];
                    var title = model.titles[i];
                    Debug.Assert(article != null, nameof(article) + " != null");
                    article.ExternalArticles.Add(new ExternalArticle()
                    {
                        CreateDateTime = DateTime.Now,
                        State = ExternalArticleState.Suggested,
                        Title = title,
                        Url = link,
                    }); //todo: autorize user and save suggester
                }
            }

            await db.SaveChangesAsync();


            return RedirectToAction("AddImageAndExternalLink");


        }

        public async Task<ActionResult> AddImageAndExternalLink(int? id)
        {
            if (id.HasValue)
            {
                return View(await db.Articles.FindAsync(id));
            }
            var article = await db.Articles.FirstAsync(x => x.ImageSuggests.Count == 0 || x.ExternalArticles.Count == 0);
            return View(article);
        }

        public async Task<ActionResult> SetImageAndExternalLink(int? id)
        {
            if (id.HasValue)
            {
                return View(await db.Articles.FindAsync(id));
            }

            var article = await db.Articles.FirstAsync(x =>
                ((x.ImageUrl == null || x.ImageUrl == "") && x.ImageSuggests.Count > 1) ||
                x.ExternalArticles.Any(y => y.State == ExternalArticleState.Suggested));
            return View(article);
        }

        [HttpPost]
        public async Task<ActionResult> SetImageAndExternalLink(SetPhotoAndExternalLinksViewModel model)
        {
            var image = await db.ImageSuggests.FindAsync(model.Id2);
            var article = image?.Article;

            if (image != null & article != null) article.ImageUrl = image.ImageUrl;
            if (model.links != null && model.state != null)
            {

                foreach (var link in model.links)
                {
                    var l = await db.ExternalArticles.FindAsync(link);
                    if (l != null) l.State = model.state.Value;
                }
            }

            await db.SaveChangesAsync();
            return RedirectToAction("SetImageAndExternalLink");
        }

    }
}