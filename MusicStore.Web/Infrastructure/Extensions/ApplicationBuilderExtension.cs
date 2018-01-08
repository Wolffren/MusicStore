using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Web.Infrastructure.Extensions
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Entity;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseDatabaseMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<MusicStoreDbContext>().Database.Migrate();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                Task.Run(async () =>
                {

                    var roleAdminExists = await roleManager.RoleExistsAsync(RoleConstants.AdministratorRole);
                    if (!roleAdminExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = RoleConstants.AdministratorRole
                        });
                    }
                    var _user = await userManager.FindByEmailAsync("admin@abv.bg");
                    // check if the user exists
                    if (_user == null)
                    {
                        //Here you could create the super admin who will maintain the web app
                        var poweruser = new User
                        {
                            UserName = "Admin",
                            Email = "admin@abv.bg",
                        };
                        string adminPassword = "admin123";

                        var createPowerUser = await userManager.CreateAsync(poweruser, adminPassword);
                        if (createPowerUser.Succeeded)
                        {
                            //here we tie the new user to the role
                            await userManager.AddToRoleAsync(poweruser, RoleConstants.AdministratorRole);

                        }
                    }

                }).Wait();


               
            }

            return app;
        }
    }
}
