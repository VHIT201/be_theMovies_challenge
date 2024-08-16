

namespace Common.Domains.Entities
{
    public abstract class SystemLogEntity<T> : BaseEntity<T>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
