using System.ComponentModel.DataAnnotations;
namespace IIG.Core.Entities
{

    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public interface ICreationAudited
    {
        DateTime Created { get; set; }
    }
    public interface IModifiedAudited
    {
        DateTime? Modified { get; set; }
    }
    public interface ISoftDelete
    {
        DateTime? Deleted { get; set; }
    }

    public abstract class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; }

    }

    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }

    }

    public abstract class AuditedEntity : Entity, ICreationAudited, IModifiedAudited
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? Modified { get; set; }
    }

    public abstract class FullAuditedEntity : AuditedEntity, ICreationAudited, IModifiedAudited, ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? Deleted { get; set; }
    
    }

}
