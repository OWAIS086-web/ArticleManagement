using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArticleManagement.Startup))]
namespace ArticleManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }
    }
}
