using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Data.Entity;
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
                services.AddEntityFramework()
                        .AddSqlServer();

                services.AddScoped<BrohubContext>();

                services.SetupOptions<DbContextOptions>(options =>
                {
                    options.UseSqlServer(@"Server=(localdb)\MsSQLLocaldb;Database=Brohub;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=true");
                });
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("analyze", "api/repository/{repoUrl}", new { controller = "Home", action = "AnalyzeRepository" });
            });


            app.UseStaticFiles();
            app.ApplicationServices
               .GetService<BrohubContext>()
               .Database
               .EnsureCreated();
        }
    }
}
