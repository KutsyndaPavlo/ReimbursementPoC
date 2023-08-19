// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace ReimbursementPoC.Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),

                // let's include the role claim in the profile
                new ProfileWithRoleIdentityResource(),
                new IdentityResources.Email()
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                // the api requires the role claim
                new ApiResource("apiscope", "The API scope", new[] { JwtClaimTypes.Role })
            };


        public static IEnumerable<Client> Clients(string url)
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { url },
                    AllowedScopes = { "openid", "profile", "email", "apiscope" },
                    RedirectUris = { $"{url}/authentication/login-callback" },
                    PostLogoutRedirectUris = { $"{url}/" },
                    Enabled = true
                },

                    //new Client
                    //{
                    //    ClientId = "oidcMVCApp",
                    //    ClientName = "Sample ASP.NET Core MVC Web App",
                    //    ClientSecrets = new List<Secret> { new Secret("ProCodeGuide".Sha256()) },

                    //    AllowedGrantTypes = GrantTypes.Code,
                    //    RedirectUris = new List<string> { $"https://{url}/signin-oidc" },
                    //    AllowedScopes = new List<string>
                    //    {
                    //        IdentityServerConstants.StandardScopes.OpenId,
                    //        IdentityServerConstants.StandardScopes.Profile,
                    //        IdentityServerConstants.StandardScopes.Email,
                    //        "apiscope"
                    //    },

                    //    RequirePkce = true,
                    //    AllowPlainTextPkce = false
                    //},
                new Client
                    {
                        // Don't use RPO if you can prevent it. We use it here
                        // because it's the easiest way to demo with users.
                        ClientId = "legacy-rpo",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        AllowAccessTokensViaBrowser = false,
                        RequireClientSecret = false,
                        AllowedScopes = {  "openid", "profile", "email", "apiscope"},

                    },
                    //new Client
                    //{
                    //    ClientId = "weatherApi",
                    //    ClientName = "ASP.NET Core Weather Api",
                    //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //    ClientSecrets = new List<Secret> { new Secret("ProCodeGuide".Sha256()) },
                    //    AllowedScopes = new List<string> {  "openid", "profile", "email", "apiscope" }
                    //},
            };

        }

    }
}