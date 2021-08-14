using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bài_tập_Quản_lý_Project.Startup))]
namespace Bài_tập_Quản_lý_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
