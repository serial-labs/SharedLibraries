using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestIdentity.Startup))]
namespace TestIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
