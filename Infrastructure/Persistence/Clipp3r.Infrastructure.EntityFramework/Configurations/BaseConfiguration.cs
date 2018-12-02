using Clipp3r.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clipp3r.Infrastructure.EntityFramework
{
    abstract class BaseConfiguration<TEntity, TIdentity> where TEntity : BaseEntity<TIdentity>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive)
                .IsRequired();
        }
    }
}
