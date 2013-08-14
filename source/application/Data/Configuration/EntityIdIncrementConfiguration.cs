using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration
{
    
    /// <summary>
    /// Automatically configure Id generation option for all entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityIdIncrementConfiguration<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity: Entity
    {
        public EntityIdIncrementConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}