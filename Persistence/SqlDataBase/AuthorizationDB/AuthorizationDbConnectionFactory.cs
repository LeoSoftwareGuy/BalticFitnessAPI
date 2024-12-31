using Npgsql;

namespace Persistence.SqlDataBase.AuthorizationDB
{
    public class AuthorizationDbConnectionFactory
    {
        private readonly string _connectionString;

        public AuthorizationDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection Create()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
