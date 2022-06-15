using ArticleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleManagement.ViewModels
{
    public class ArticleListingViewModel
    {
        public List<Article> Articles { get; set; }
        public string SearchTerm { get; set; }
    }

    public class ArticleActionViewModel
    {
        public List<BlogTitle> BlogTitles { get; set; }
        public int ID { get; set; }
        public string DocURL { get; set; }
        public string ArticleName { get; set; }
        public string Note { get; set; }
        public DateTime PostingDate { get; set; }
        public string BlogSiteTitle { get; set; }
    }
}