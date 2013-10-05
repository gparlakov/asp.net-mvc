using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Teleritter.Startup))]
namespace Teleritter
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
