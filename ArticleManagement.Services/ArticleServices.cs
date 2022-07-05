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
    public class ArticleServices
    {
        #region Singleton
        public static ArticleServices Instance
        {
            get
            {
                if (instance == null) instance = new ArticleServices();
                return instance;
            }
        }
        private static ArticleServices instance { get; set; }
        private ArticleServices()
        {
        }
        #endregion

        public Article GetArticle(int ID)
        {
            using (var context = new AMContext())
            {
                return context.Articles.Find(ID);
            }
        }


        public List<Article> GetArticles(string SearchTerm = "")
        {
            List<Article> Articles = null;
            using (var context = new AMContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Articles = context.Articles.Where(x => x.ArticleName != null && x.ArticleName.ToLower()
                                                         .Contains(SearchTerm.ToLower())).ToList();
                }
                else
                {
                    Articles = context.Articles.ToList();
                }
            }
            return Articles;
        }


        public List<Article> GetPendingArticles(string SearchTerm = "")
        {
            List<Article> Articles = null;
            using (var context = new AMContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Articles = context.Articles.Where(x => x.Status == "Pending" && x.ArticleName != null && x.ArticleName.ToLower()
                                                         .Contains(SearchTerm.ToLower())).ToList();
                }
                else
                {
                    Articles = context.Articles.Where(x=>x.Status == "Pending").ToList();
                }
            }
            return Articles;
        }

        public List<Article> GetArticlesViaUserName(string UserName, string SearchTerm = "")
        {
            List<Article> Articles = null;
            using (var context = new AMContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Articles = context.Articles.Where(x =>  x.Name != null  && x.Name.ToLower()
                                                         .Contains(SearchTerm.ToLower()) && x.Name == UserName).ToList();
                }
                else
                {
                    Articles = context.Articles.Where(x=>x.Name == UserName).ToList();
                }
            }
            return Articles;
        }

        public List<Article> GetPendingArticlesViaUserName(string SearchTerm = "")
        {
            List<Article> Articles = null;
            using (var context = new AMContext())
            {
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Articles = context.Articles.Where(x => x.Status == "Pending" && x.Name != null && x.Name.ToLower()
                                      .Contains(SearchTerm.ToLower())).ToList();
                }
                else
                {
                    Articles = context.Articles.Where(x => x.Status == "Pending" && x.Name == SearchTerm).ToList();
                }
            }
            return Articles;
        }


        public void SaveArticle(Article Article)
        {
            using (var context = new AMContext())
            {
                context.Articles.Add(Article);
                context.SaveChanges();
            }
        }

        public void UpdateArticle(Article Article)
        {
            using (var context = new AMContext())
            {
                context.Entry(Article).State = EntityState.Modified;
                context.SaveChanges();
            }
        }



        public void DeleteArticle(int ID)
        {
            using (var context = new AMContext())
            {
                var Article = context.Articles.Find(ID);
                context.Articles.Remove(Article);
                context.SaveChanges();
            }
        }
    }
}
