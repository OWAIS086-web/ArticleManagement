using Microsoft.AspNet.Identity.EntityFramework;
using ArticleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Database
{
    public class AMContext :IdentityDbContext<User>,IDisposable
    {
        public AMContext() : base("AMConnectionStrings")
        {

        }

        public static AMContext Create()
        {
            return new AMContext();
        }

    }
}
