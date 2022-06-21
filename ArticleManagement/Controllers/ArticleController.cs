using ArticleManagement.ViewModels;
using ArticleManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ArticleManagement.Entities;
using Microsoft.AspNet.Identity.Owin;

namespace ArticleManagement.Controllers
{
    public class ArticleController : Controller
    {
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private AMRolesManager _rolesManager;
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }
        public ArticleController()
        {
        }



        public ArticleController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Article
        public ActionResult Index(string searchterm = "")
        {

            ArticleListingViewModel model = new ArticleListingViewModel();
            if (User.IsInRole("Copywriter") == true)
            {
                searchterm = User.Identity.Name;
                var user = UserManager.FindByEmail(searchterm);
                model.Articles = ArticleServices.Instance.GetArticlesViaUserName(user.Name);

            }
            else
            {
                model.Articles = ArticleServices.Instance.GetArticles(searchterm);
            }
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
                model.Name = article.Name;
                model.ArticleName = article.ArticleName;
                model.BlogSiteTitle = article.BlogSiteTitle;
                model.DocURL = article.DocURL;
                model.Note = article.Note;
                model.PostingDate = article.PostingDate;
                model.FocusKeyWord = article.FocusKeyWord;
                model.Status = article.Status;
                model.KeywordLink = article.KeywordLink;
                model.Words = article.Words;
                model.PayPerWord = article.PayPerWord;
                
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
            try
            {
                if (model.ID != 0) //update record
                {
                    var article = ArticleServices.Instance.GetArticle(model.ID);

                    article.ID = model.ID;
                    article.ArticleName = model.ArticleName;
                    article.BlogSiteTitle = model.BlogSiteTitle;
                    if (model.DocURL != null)
                    {
                        article.DocURL = model.DocURL;
                    }
                    article.Note = model.Note;
                    article.PostingDate = model.PostingDate;
                    article.Words = model.Words;
                    article.PayPerWord = model.PayPerWord;
                    article.FocusKeyWord = model.FocusKeyWord;
                    article.KeywordLink = model.KeywordLink;
                    article.Status = model.Status;
                    var userID = User.Identity.Name;
                    var user = UserManager.FindByEmail(userID);
                    article.Name = user.Name;
                    ArticleServices.Instance.UpdateArticle(article);

                }
                else
                {
                    var article = new Article();
                    article.ArticleName = model.ArticleName;
                    article.BlogSiteTitle = model.BlogSiteTitle;
                    if (model.DocURL != null)
                    {
                        article.DocURL = model.DocURL;
                    }
                    article.Note = model.Note;
                    article.PostingDate = model.PostingDate;
                    article.Status = "Pending";
                    article.Words = model.Words;
                    article.PayPerWord = model.PayPerWord;
                    article.FocusKeyWord = model.FocusKeyWord;
                    article.KeywordLink = model.KeywordLink;
                    var userID = User.Identity.Name;
                    var user = UserManager.FindByEmail(userID);
                    article.Name = user.Name;
                    ArticleServices.Instance.SaveArticle(article);

                }

            }
            catch (Exception ex)
            {

                throw;
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
        public ActionResult Delete(ArticleActionViewModel model)
        {

           

            if (model.ID != 0) //we are trying to delete a record
            {
                var article = ArticleServices.Instance.GetArticle(model.ID);
                ArticleServices.Instance.DeleteArticle(article.ID);
            }
           
            return RedirectToAction("Index", "Article");
        }


        public ActionResult Post(int ID)
        {
            if (ID != 0)
            {
                var article = ArticleServices.Instance.GetArticle(ID);
                article.Status = "Posted";
                ArticleServices.Instance.UpdateArticle(article);
            }
            return RedirectToAction("Index", "Article");
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
          
            string path = Server.MapPath("~") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}