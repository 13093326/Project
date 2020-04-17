using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevisionApplication.Helpers;
using RevisionApplication.Repository;
using System;

namespace RevisionApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure database. 
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Dependancy injection for database repository. 
            services.AddTransient<ICommonHelper, CommonHelper>();
            services.AddTransient<IMultipleChoiceHelper, MultipleChoiceHelper>();
            services.AddTransient<IFlashCardHelper, FlashCardHelper>();
            services.AddTransient<IQuestionHelper, QuestionHelper>();
            services.AddTransient<IQuestionRatingRepository, QuestionRatingRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IReportHelper, ReportHelper>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ISettingsHelper, SettingsHelper>();
            services.AddTransient<ITestHelper, TestHelper>();
            services.AddTransient<ITestSetRepository, TestSetRepository>();
            services.AddTransient<ITestQuestionRepository, TestQuestionRepository>();
            services.AddTransient<IUnitHelper, UnitHelper>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IUserSettingsRepository, UserSettingsRepository>();

            // Set up MVC and settings. 
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Handle errors based on developement or release. 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            // Add middleware. 
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                // Default route 
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            }
            );
        }
    }
}
