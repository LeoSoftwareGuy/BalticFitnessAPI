using Npgsql;

namespace Persistence.SqlDataBase
{
    public class TrainingsDbConnectionFactory
    {
        private readonly string _connectionString;

        public TrainingsDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection Create()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();  
            return connection;
        }
    }
}
