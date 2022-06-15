using ArticleManagement.ViewModels;
using ArticleManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ArticleManagement.Entities;

namespace ArticleManagement.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index(string searchterm)
        {
            ArticleListingViewModel model = new ArticleListingViewModel();
            model.Articles = ArticleServices.Instance.GetArticles(searchterm);
            return View(model);
        }



        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            ArticleActionViewModel model = new ArticleActionViewModel();
            model.BlogTitles = BlogTitleServices.Instance.GetBlogTitles();
            if (ID != 0)
            {
                var article = ArticleServices.Instance.GetArticle(ID);
                model.ID = article.ID;
                model.ArticleName = article.ArticleName;
                model.BlogSiteTitle = article.BlogSiteTitle;
                model.DocURL = article.DocURL;
                model.Note = article.Note;
                model.PostingDate = article.PostingDate;

            }
            else
            {

            }
            return PartialView("_Action", model);
        }


        [HttpPost]
        public ActionResult Action(ArticleActionViewModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (model.ID != 0) //update record
            {
                var article = ArticleServices.Instance.GetArticle(model.ID);

                article.ID = model.ID;
                article.ArticleName = model.ArticleName;
                article.BlogSiteTitle = model.BlogSiteTitle;
                article.DocURL = model.DocURL;
                article.Note = model.Note;
                article.PostingDate = model.PostingDate;

                ArticleServices.Instance.UpdateArticle(article);

            }
            else
            {
                var article = new Article();
                article.ArticleName = model.ArticleName;
                article.BlogSiteTitle = model.BlogSiteTitle;
                article.DocURL = model.DocURL;
                article.Note = model.Note;
                article.PostingDate = model.PostingDate;

                ArticleServices.Instance.SaveArticle(article);

            }

            return RedirectToAction("Index", "Article");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int ID)
        {
            ArticleActionViewModel model = new ArticleActionViewModel();
            var article = ArticleServices.Instance.GetArticle(ID);
            model.ID = article.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(ArticleActionViewModel model)
        {

            IdentityResult result = null;
            JsonResult json = new JsonResult();


            if (model.ID != 0) //we are trying to delete a record
            {
                var article = ArticleServices.Instance.GetArticle(model.ID);
                ArticleServices.Instance.DeleteArticle(article.ID);
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Role." };
            }

            return json;
        }
    }
}