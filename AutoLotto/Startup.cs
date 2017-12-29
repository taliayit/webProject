using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoLotto.Startup))]
namespace AutoLotto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
