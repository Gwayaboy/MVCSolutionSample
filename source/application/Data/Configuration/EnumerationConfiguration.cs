using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Inflector;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration
{
    public class EnumerationConfiguration<TEnumeration> : EntityTypeConfiguration<TEnumeration>
       where TEnumeration : Enumeration<TEnumeration>
    {

        protected virtual string KeyColumnName { get { return "Id"; } }
        protected virtual string NameColumnName { get { return "Name"; } }
        protected virtual string TableName { get { return typeof(TEnumeration).Name.Pluralize(); } }

        public EnumerationConfiguration()
        {
            ConfigureEnumeration();
        }

        private void ConfigureEnumeration()
        {
            HasKey(x => x.Value);
            Property(x => x.Value)
                .HasColumnName(KeyColumnName)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.DisplayName).HasColumnName(NameColumnName);

            ToTable(TableName);
        }
    }
}
