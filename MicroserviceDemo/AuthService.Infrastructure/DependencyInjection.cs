using AuthService.Infrastructure.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configurationBuilder)
        {

            //add db context for ConfigurationDbContext
            services.AddDbContext<ConfigurationDbContext>(options =>
            {
                options.UseSqlServer(configurationBuilder.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).
     AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); ;

            //add identity server
            services.AddIdentityServer(options =>
            {
                options.KeyManagement.Enabled = false;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;

            }).AddDeveloperSigningCredential().AddAspNetIdentity<IdentityUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(configurationBuilder.GetConnectionString("DefaultConnection"));
            }).AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(configurationBuilder.GetConnectionString("DefaultConnection"));

                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            });


            return services;
        }
    }
}
