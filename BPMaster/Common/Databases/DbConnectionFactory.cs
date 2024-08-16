using System.Data;
using System.Data.SqlClient;
using Common.Application.Enums;
using Common.Application.Settings;
using Npgsql;


namespace Common.Databases
{
    public class DbConnectionFactory
    {
        public static IDbConnection GetConnection(DatabaseSetting setting)
        {
            IDbConnection connection = setting.DatabaseType switch
            {
                DatabaseType.SqlServer => new SqlConnection(setting.ConnectionString),
                DatabaseType.Postgres => new NpgsqlConnection(setting.ConnectionString),
                _ => throw new NotImplementedException(),
            };

            return connection;
        }
    }
}
