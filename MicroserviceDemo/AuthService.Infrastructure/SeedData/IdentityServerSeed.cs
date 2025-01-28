using AuthService.Infrastructure.DB;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.SeedData
{
    public static class IdentityServerSeed
    {
        public static void InitializeIdentityServerConfiguration(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                // Kiểm tra và thêm dữ liệu nếu chưa có
                if (!configurationDbContext.Clients.Any())
                {
                    var clients = Config.GetClients();
                    foreach (var client in clients)
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                }

                if (!configurationDbContext.ApiResources.Any())
                {
                    var apiResources = Config.GetApiResources();
                    foreach (var apiResource in apiResources)
                    {
                        configurationDbContext.ApiResources.Add(apiResource.ToEntity());
                    }
                }

                if (!configurationDbContext.IdentityResources.Any())
                {
                    var identityResources = Config.IdentityResources;
                    foreach (var identityResource in identityResources)
                    {
                        configurationDbContext.IdentityResources.Add(identityResource.ToEntity());
                    }
                }

                configurationDbContext.SaveChanges();
            }
        }
    }
}
