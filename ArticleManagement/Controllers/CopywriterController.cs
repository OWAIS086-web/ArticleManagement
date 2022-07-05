using ArticleManagement.Services;
using ArticleManagement.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleManagement.Controllers
{
    public class CopywriterController : Controller
    {
        private AMSignInManager _signInManager;
        private AMRolesManager _rolesManager;
        private AMUserManager _userManager;
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
        // GET: Copywriter
        public ActionResult Dashboard()
        {
            CopywriterViewModel model = new CopywriterViewModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.Articles = ArticleServices.Instance.GetArticlesViaUserName(user.Name, "");
            model.SignInUser = user;
            return View(model);
        }


        [HttpPost]
        public ActionResult Dashboard(string SearchTerm = "")
        {
            CopywriterViewModel model = new CopywriterViewModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.Articles = ArticleServices.Instance.GetArticlesViaUserName(user.Name, SearchTerm);
            model.SignInUser = user;
            return View(model);
        }



    }
}