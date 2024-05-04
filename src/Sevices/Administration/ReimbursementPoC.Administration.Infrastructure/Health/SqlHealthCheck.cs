using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReimbursementPoC.Administration.Infrastructure.Health
{
    public  static class HealthCheck
    {
        public static IHealthChecksBuilder AddHealthChecks(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            // https://www.milanjovanovic.tech/blog/health-checks-in-asp-net-core
            var connectionString = configuration.GetConnectionString("Db");
            builder.AddSqlServer(connectionString);

            return builder;
        }
    }
}
