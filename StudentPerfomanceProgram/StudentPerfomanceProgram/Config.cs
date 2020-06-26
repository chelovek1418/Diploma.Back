using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using StudentPerfomance.IdentityServer.Data.Constants;
using StudentPerfomance.IdentityServer.Models;
using System.Collections.Generic;

namespace StudentPerfomance.Api
{
    public static class Config
    {
        private const string ClientUrl = "http://localhost:4200";

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
            };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource> 
        { 
            new ApiResource(IdentityData.StudentPerfomanceApi, new [] { JwtClaimTypes.Role })
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "angular_spa",

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,

                RedirectUris = { ClientUrl },
                PostLogoutRedirectUris = { ClientUrl },
                AllowedCorsOrigins = { ClientUrl },

                AllowedScopes =
                {
                    IdentityData.StudentPerfomanceApi,
                    IdentityServerConstants.StandardScopes.OpenId,
                },

                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
            }
        };
    }
}
