using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RevisionApplication.Repository;

[assembly: HostingStartup(typeof(RevisionApplication.Areas.Identity.IdentityHostingStartup))]
namespace RevisionApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<AppDbContext>();
            });
        }
    }
}