using ArticleManagement.Services;
using ArticleManagement.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using ArticleManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogTitleManagement.Controllers
{
    public class BlogTitleController : Controller
    {
        // GET: BlogTitle
        public ActionResult Index(string searchterm)
        {
            BlogTitleListingViewModel model = new BlogTitleListingViewModel();
            model.BlogTitles = BlogTitleServices.Instance.GetBlogTitles(searchterm);
            return View(model);
        }



        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            BlogTitleActionViewModel model = new BlogTitleActionViewModel();
            if (ID != 0)
            {
                var BlogTitle = BlogTitleServices.Instance.GetBlogTitle(ID);
                model.ID = BlogTitle.ID;
                model.TitleName = BlogTitle.TitleName;
                
            }
            else
            {

            }
            return PartialView("_Action", model);
        }


        [HttpPost]
        public ActionResult Action(BlogTitleActionViewModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (model.ID != 0) //update record
            {
                var BlogTitle = BlogTitleServices.Instance.GetBlogTitle(model.ID);

                BlogTitle.ID = model.ID;
                BlogTitle.TitleName = model.TitleName;
              
                BlogTitleServices.Instance.UpdateBlogTitle(BlogTitle);

            }
            else
            {
                var BlogTitle = new BlogTitle();
                BlogTitle.TitleName = model.TitleName;

                BlogTitleServices.Instance.SaveBlogTitle(BlogTitle);

            }

            //json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            return RedirectToAction("Index","BlogTitle");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int ID)
        {
            BlogTitleActionViewModel model = new BlogTitleActionViewModel();
            var BlogTitle = BlogTitleServices.Instance.GetBlogTitle(ID);
            model.ID = BlogTitle.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(BlogTitleActionViewModel model)
        {

            IdentityResult result = null;
            JsonResult json = new JsonResult();


            if (model.ID != 0) //we are trying to delete a record
            {
                var BlogTitle = BlogTitleServices.Instance.GetBlogTitle(model.ID);
                BlogTitleServices.Instance.DeleteBlogTitle(BlogTitle.ID);
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Role." };
            }

            return json;
        }
    }
}