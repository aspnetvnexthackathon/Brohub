using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.StaticFiles;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace Brohub
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            app.UseServices(services =>
            {
                services.AddMvc();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller}/{action}");
            });

            app.UseStaticFiles();
        }
    }
}
