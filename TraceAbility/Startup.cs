using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestABC.Startup))]
namespace TestABC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
