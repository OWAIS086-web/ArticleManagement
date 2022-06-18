using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Entities
{
    public class Article:BaseEntity
    {
        public string Name { get; set; }
        public string DocURL { get; set; }
        public string ArticleName { get; set; }
        public string Note { get; set; }
        public DateTime PostingDate { get; set; }
        public string BlogSiteTitle { get; set; }
        public float Words { get; set; }
        public float PayPerWord { get; set; }

    }
}
