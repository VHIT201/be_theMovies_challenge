using Dapper.Contrib.Extensions;

namespace Common.Domains.Entities
{
	public abstract class BaseEntity<T>
	{
        [ExplicitKey]
        public virtual required T Id { get; set; }
	}
}
