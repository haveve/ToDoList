using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace ToDoList.Models.DapperClasses
{
    public class DapperConnectionProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection Connect() => new SqlConnection(_connectionString);
    }
}
