namespace Clipp3r.Core.Entities
{
    public abstract class BaseEntity<T> : Entity
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
    }
}
