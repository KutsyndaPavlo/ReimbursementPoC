// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Identity.API.Data;
using ReimbursementPoC.Identity.API.Models;
using System.Security.Claims;

namespace ReimbursementPoC.Identity.API
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!roleManager.RoleExistsAsync("admin").Result)
                    {
                        roleManager.CreateAsync(new IdentityRole("admin")).GetAwaiter().GetResult();
                    }

                    if (!roleManager.RoleExistsAsync("vendor").Result)
                    {
                        roleManager.CreateAsync(new IdentityRole("vendor")).GetAwaiter().GetResult();
                    }

                    if (!roleManager.RoleExistsAsync("customer").Result)
                    {
                        roleManager.CreateAsync(new IdentityRole("customer")).GetAwaiter().GetResult();
                    }

                    context.SaveChanges();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null)
                    {
                        alice = new ApplicationUser
                        {
                            UserName = "alice"
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }


                        context.SaveChanges();

                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                       }).Result;


                        context.SaveChanges();

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(alice, "admin").Result)
                        {
                            userMgr.AddToRoleAsync(alice, "admin").GetAwaiter().GetResult();
                        };

                        context.SaveChanges();
                    }

                    var bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null)
                    {
                        bob = new ApplicationUser
                        {
                            UserName = "bob"
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;


                        context.SaveChanges();

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }).Result;

                        context.SaveChanges();

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }


                        if (!userMgr.IsInRoleAsync(bob, "vendor").Result)
                        {
                            userMgr.AddToRoleAsync(bob, "vendor").GetAwaiter().GetResult();
                        };

                        context.SaveChanges();

                    }

                    var tom = userMgr.FindByNameAsync("tom").Result;
                    if (tom == null)
                    {
                        tom = new ApplicationUser
                        {
                            UserName = "tom"
                        };
                        var result = userMgr.CreateAsync(tom, "Pass123$").Result;

                        context.SaveChanges();

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(tom, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Tom Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Tom"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "tomSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://tom.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;

                        context.SaveChanges();


                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(tom, "customer").Result)
                        {
                            userMgr.AddToRoleAsync(tom, "customer").GetAwaiter().GetResult();
                        };

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
