namespace IIG.Core.Common.Models
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public interface IBaseEntity<T> where T : new()
    {
        public T Id { get; set; }
    }

    public interface ITrackingEntity
    {
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
