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
    public class ExternalArticleController : Controller
    {
        MatlabDb db = new MatlabDb();

        // GET: Article
        [HttpPost]
        public async Task<ActionResult> AddExternalLink(List<string> links,List<string>titles, int id)
        {
            var article = await db.Articles.FindAsync(id);

            for (int i = 0; i < links.Count(); i++)
            {
                var link = links[i];
                var title = titles[i];
                Debug.Assert(article != null, nameof(article) + " != null");
                article.ExternalArticles.Add(new ExternalArticle()
                {
                    CreateDateTime = DateTime.Now,
                    State = ExternalArticleState.Suggested,
                    Title = title,
                    Url = link,
                });//todo: autorize user and save suggester
            }

            await db.SaveChangesAsync();
            return RedirectToAction("AddExternalLink");

            //foreach (string link in links)
            //{
            //    int i = links
            //    article.ExternalArticles.Add(new ExternalArticle()
            //    {
            //        CreateDateTime = DateTime.Now,
            //        State = ExternalArticleState.Suggested,
            //        //Title = 
            //    });
            //}

        }

        public async Task<ActionResult> AddExternalLink(int? id)
        {
            if (id.HasValue)
            {
                return View(await db.Articles.FindAsync(id));
            }
            var article = await db.Articles.FirstAsync(x => x.ExternalArticles.Count == 0);
            return View(article);
        }

        public async Task<ActionResult> SetExternalLink(int? id)
        {
            if (id.HasValue)
            {
                return View(await db.Articles.FindAsync(id));
            }
            var article = await db.Articles.FirstAsync(x => x.ExternalArticles.Any(y=>y.State==ExternalArticleState.Suggested));
            return View(article);
        }

        [HttpPost]
        public async Task<ActionResult> SetExternalLink(IEnumerable<int> links,ExternalArticleState state)
        {
            foreach (var link in links)
            {
                var l = await db.ExternalArticles.FindAsync(link);
                if (l != null) l.State = state;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("SetExternalLink");
        }
    }
}