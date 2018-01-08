

namespace MusicStore.Web
{
    using AutoMapper;
    using Data;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Entity;
    using MusicStore.Services.Implementations;
    using MusicStore.Services.Interfaces;
    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MusicStoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(op=>
                {
                    op.Password.RequireUppercase = false;
                    op.Password.RequireNonAlphanumeric = false;
                })

            
                .AddEntityFrameworkStores<MusicStoreDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "614472198445-67ncmnkkollbts65v7rmgf4n0qf7drjs.apps.googleusercontent.com";
                googleOptions.ClientSecret = "weq4aKB8QVZH6oax5PUK9nsm";
            });

            services.AddSession();
            // Add application services.
            services.AddDomainServices();
           services.AddTransient<IEmailSender, EmailSender>();
            //services.AddTransient<IHomeService, HomeService>();
            //services.AddTransient<IStoreService, StoreService>();
            //services.AddTransient<IShoppingCartService, ShoppingCartService>();
            //services.AddTransient<ICheckoutService, CheckoutService>();

            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigrations();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}",
                    defaults: new { action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
