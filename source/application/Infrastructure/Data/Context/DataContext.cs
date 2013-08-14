using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using Seterlund.CodeGuard;
using Panzea.DonorSpace.Core.Domain;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Context
{
    public class DataContext : DbContext
    {
        private IDictionary<MethodInfo, object> _configurations;

        public IDbSet<Charity> Charities { get; set; }
        public IDbSet<Donor> Donors { get; set; }
        public IDbSet<Administrator> Administrators { get; set; }

        public DataContext(string nameOrConnectionString,IDictionary<MethodInfo, object> configurations)
            : base(nameOrConnectionString)
        {
            Guard.That(configurations).IsNotNull();
            _configurations = configurations;
        }

        public IQueryable<T> CreateQueryWith<T>(IEnumerable<Expression<Func<T, object>>> propertySelectors = null)
            where T : class
        {
            IQueryable<T> set = Set<T>();

            if (propertySelectors != null && propertySelectors.Any())
            {
                set = propertySelectors.Aggregate(set, (current, propertySelector) => current.Include(propertySelector));
            }

            return set;
        }
        
        /// <summary>
        /// Auto fluent configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Guard.That(modelBuilder).IsNotNull();

            foreach (var configuration in _configurations)
            {
                configuration.Key.Invoke(modelBuilder.Configurations, new[] {configuration.Value});
            }

            base.OnModelCreating(modelBuilder);
        }


        public virtual void MarkAsModified<TEntity>(TEntity instance)
            where TEntity : class
        {
            Entry(instance).State = EntityState.Modified;
        }
    }
}
