using System;
using System.Data.Entity;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Intrigma.DonorSpace.Core.Domain;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context
{
    public class DataContext : DbContext
    {
        private readonly Type[] _modelMappingConfigurationTypes;

        public IDbSet<Charity> Charities { get; set; }
        public IDbSet<Donor> Donors { get; set; }
        public IDbSet<Administrator> Administrators { get; set; }
        public IDbSet<Role> Roles { get; set; }

        internal DataContext(Type[] modelMappingConfigurationTypes, string nameOrConnectionString = null)
            :base(nameOrConnectionString ?? "DefaultConnection")
        {
            Guard.That(modelMappingConfigurationTypes).IsNotNull();
            _modelMappingConfigurationTypes = modelMappingConfigurationTypes;
        }
        
        internal IQueryable<T> CreateQueryWith<T>(params Expression<Func<T, object>>[] includedPropertySelectors)
            where T : class
        {
            IQueryable<T> set = Set<T>();

            if (includedPropertySelectors != null && includedPropertySelectors.Any())
            {
                set = includedPropertySelectors.Aggregate(set, (current, propertySelector) => current.Include(propertySelector));
            }

            return set;
        }

        internal void Remove<TEntity>(TEntity entity)
           where TEntity : class
        {
            var entry = Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                var dbSet = Set<TEntity>();
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }

        internal void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entry = Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        internal void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entry = Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                Set<TEntity>().Add(entity);
            }
        }
        
        /// <summary>
        /// Auto fluent configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Guard.That(modelBuilder).IsNotNull();
            
            modelBuilder.Configurations.AddRange(_modelMappingConfigurationTypes);

            base.OnModelCreating(modelBuilder);
        }

     
       
    }
}
