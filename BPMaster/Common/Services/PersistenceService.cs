using System.Data;


namespace Common.Services
{
    public abstract class PersistenceService(IServiceProvider services, IDbConnection connection) : BaseService(services)
    {
        protected readonly IDbConnection _connection = connection;
    }
}
