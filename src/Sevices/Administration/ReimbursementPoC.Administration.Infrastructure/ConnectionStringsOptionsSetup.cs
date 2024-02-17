using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ReimbursementPoC.Administration.Infrastructure;

namespace ReimbursementPoC.Administration.API
{
    public class ConnectionStringsOptionsSetup : IConfigureOptions<ConnectionStringsOptions>
    {
        private const string SectionName = "ConnectionStrings";
        private readonly IConfiguration _configuration;

        public ConnectionStringsOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(ConnectionStringsOptions options)
        {
            _configuration
                .GetSection(SectionName)
                .Bind(options);
        }
    }
}
