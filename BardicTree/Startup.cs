using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BardicTree.Startup))]
namespace BardicTree
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
