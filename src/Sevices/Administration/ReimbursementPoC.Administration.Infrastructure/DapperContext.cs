using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ReimbursementPoC.Administration.Infrastructure
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Db");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task Test()
        {
            var query = "SELECT * FROM Companies Where name = @filter";
            using (var connection = this.CreateConnection())
            {
                var param = new DynamicParameters();
                param.Add("filter", "filterValue");
                var result = await connection.QueryAsync<object>(query);
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var data = result.ToList();
            }
        }
    }

    
}
