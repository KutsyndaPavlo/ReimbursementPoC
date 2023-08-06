// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
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
            };

        }
            
    }
}