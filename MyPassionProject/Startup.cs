using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPassionProject.Startup))]
namespace MyPassionProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
