using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaiTap6_64132989_IsAuth.Startup))]
namespace BaiTap6_64132989_IsAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
