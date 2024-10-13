using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaiTap6_Auth.Startup))]
namespace BaiTap6_Auth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
