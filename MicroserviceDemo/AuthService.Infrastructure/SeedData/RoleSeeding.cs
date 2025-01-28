using AuthService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.SeedData
{
    public static class RoleSeeding
    {
        public static void InitializeRoleConfiguration(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!roleManager.Roles.Any())
                {
                    roleManager.CreateAsync(new IdentityRole { Name = Role.Admin }).Wait();
                    roleManager.CreateAsync(new IdentityRole { Name = Role.User }).Wait();
                }
            }
        }
    }
}
