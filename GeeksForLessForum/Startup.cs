using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeeksForLessForum.Startup))]
namespace GeeksForLessForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
