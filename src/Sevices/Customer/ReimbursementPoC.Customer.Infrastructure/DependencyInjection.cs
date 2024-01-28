using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Infrastructure.Persistence;
using ReimbursementPoC.Customer.Infrastructure.Services;
namespace ReimbursementPoC.Customer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Db");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ReimbursementPoC"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                            sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                        }));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();

            //services
            //    .AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();


            //services.AddTransient<IIdentityService, IdentityService>();
            //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            //services.AddAuthorization(options =>
            //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

            return services;
        }
    }
}