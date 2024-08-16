using Common.Application.Settings;
using Common.Databases.Interfaces;

namespace Common.Databases.PostgresSql
{
    public class PostgresSqlGenerator: SqlGenerator
    {
        protected override IDictionaryDataType DictionaryDataType => new PostgresSqlDictionaryDataType();
    }
}
