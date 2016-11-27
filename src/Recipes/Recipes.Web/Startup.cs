using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Recipes.Web.Startup))]
namespace Recipes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
