using Common.Databases.Interfaces;

namespace Common.Databases.PostgresSql
{
    public class PostgresSqlDictionaryDataType : IDictionaryDataType
    {
        private static readonly Dictionary<Type, string> PostgresSqlMap = new()
        {
            { typeof(Guid), "uuid" },
            { typeof(Guid?), "uuid" },
            { typeof(string), "varchar(255)" },
            { typeof(DateTime), "timestamp with time zone" },
            { typeof(byte[]), "bytea[]" },
            { typeof(bool), "boolean" },
            { typeof(int), "int" }
        };

        Dictionary<Type, string> IDictionaryDataType.Dictionary => PostgresSqlMap;
    }
}
