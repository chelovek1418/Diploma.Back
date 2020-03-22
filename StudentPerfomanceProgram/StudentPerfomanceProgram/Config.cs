using IdentityServer4;
using IdentityServer4.Models;
using StudentPerfomance.IdentityServer.Data.Constants;
using System.Collections.Generic;

namespace StudentPerfomance.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                //new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "rc.scope",
                    UserClaims =
                    {
                        "rc.garndma"
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource> { new ApiResource(IdentityData.StudentPerfomanceApi) };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            //new Client
            //{
            //    ClientId = "client",
            //    AllowedGrantTypes = GrantTypes.ClientCredentials,
            //    ClientSecrets = { new Secret("secret".Sha256()) },
            //    AllowedScopes = { IdentityData.StudentPerfomanceApi }
            //},
            new Client
            {
                ClientId = "client_id_mvc",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                ClientSecrets = { new Secret("client_secret_mvc".Sha256()) },
                AllowedScopes =
                {
                    IdentityData.StudentPerfomanceApi,
                    //"api2",
                    IdentityServerConstants.StandardScopes.OpenId,
                    //IdentityServerConstants.StandardScopes.Profile,
                    "rc.scope"
                },

                RedirectUris = { "https://localhost:44308/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:44308/Home/Index" },

                RequireConsent = false,

                AllowOfflineAccess = true,

                //AlwaysIncludeUserClaimsInIdToken = true,
            },


            // JS Client
            new Client
            {
                ClientId = "client_id_js",

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,

                RedirectUris = { "https://localhost:44378/home/signin" },
                PostLogoutRedirectUris = { "https://localhost:44378/Home/Index" },
                AllowedCorsOrigins = { "https://localhost:44378" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityData.StudentPerfomanceApi,
                    //"api2",
                    "rc.scope"
                },

                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
            }
        };
    }
}
