using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParcelLoader.Startup))]
namespace ParcelLoader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
