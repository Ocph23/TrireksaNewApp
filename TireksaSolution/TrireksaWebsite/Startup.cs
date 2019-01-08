using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrireksaWebsite.Startup))]
namespace TrireksaWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
