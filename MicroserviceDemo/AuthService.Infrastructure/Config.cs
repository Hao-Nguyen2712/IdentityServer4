using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
      new List<IdentityResource>
      {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),

      };


        // cấu hình các scope
        public static IEnumerable<ApiScope> GetApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("admin.access", "Access to admin functionality"),
                new ApiScope("user.access", "Access to user functionality ")
            };



        // cấu hình resource được dùng bởi scopes nào
        public static IEnumerable<ApiResource> GetApiResources() =>
      new List<ApiResource>
      {
        new ApiResource("UserApi", "User API")
        {
            Scopes = { "user.access" }
        }
      };



        // cấu hình các client
        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "user.access" }
                }
            };
    }
}
