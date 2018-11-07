using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScoutSFTChallenge.UI.Startup))]
namespace ScoutSFTChallenge.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
