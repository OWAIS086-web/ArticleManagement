using ArticleManagement.Database;
using ArticleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Services
{
    public class BlogTitleServices
    {
        #region Singleton
        public static BlogTitleServices Instance
        {
            get
            {
                if (instance == null) instance = new BlogTitleServices();
                return instance;
            }
        }
        private static BlogTitleServices instance { get; set; }
        private BlogTitleServices()
        {
        }
        #endregion

        public BlogTitle GetBlogTitle(int ID)
        {
            using (var context = new AMContext())
            {
                return context.BlogTitles.Find(ID);
            }
        }


        public List<BlogTitle> GetBlogTitles(string SearchTerm = "")
        {
            List<BlogTitle> BlogTitles = null;
            using (var context = new AMContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    BlogTitles = context.BlogTitles.Where(x => x.TitleName == SearchTerm).ToList();
                }
                else
                {
                    BlogTitles = context.BlogTitles.ToList();
                }
            }
            return BlogTitles;
        }


        public void SaveBlogTitle(BlogTitle BlogTitle)
        {
            using (var context = new AMContext())
            {
                context.BlogTitles.Add(BlogTitle);
                context.SaveChanges();
            }
        }

        public void UpdateBlogTitle(BlogTitle BlogTitle)
        {
            using (var context = new AMContext())
            {
                context.Entry(BlogTitle).State = EntityState.Modified;
                context.SaveChanges();
            }
        }



        public void DeleteBlogTitle(int ID)
        {
            using (var context = new AMContext())
            {
                var BlogTitle = context.BlogTitles.Find(ID);
                context.BlogTitles.Remove(BlogTitle);
                context.SaveChanges();
            }
        }
    }
}

