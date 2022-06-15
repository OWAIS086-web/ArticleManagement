using ArticleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleManagement.ViewModels
{
    public class BlogTitleListingViewModel
    {
        public List<BlogTitle> BlogTitles { get; set; }
        public string SearchTerm { get; set; }
    }


    public class BlogTitleActionViewModel
    {
        public int ID { get; set; }
        public string TitleName { get; set; }
    }
}