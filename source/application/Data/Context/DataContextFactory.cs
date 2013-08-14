using System;
using System.Linq;
using System.Collections.Generic;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration.Conventions;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context
{
    public class DataContextFactory : IDataContextFactory
    {
        
        private readonly IDictionary<Type,MappingConfigurationTypesFinder> _finders =
            new Dictionary<Type, MappingConfigurationTypesFinder>();

 
        private readonly string _nameOrConnectionString;


        private Type[] MappingConfigurationTypes
        {
            get
            {
                return
                    _finders
                        .Values
                        .SelectMany(c => c.MappingConfigurationTypes)
                        .ToArray();
            }
        }

        public DataContextFactory(IEnumerable<MappingConfigurationTypesFinder> mappingConfigurationFinders)
        {
            Guard.That(mappingConfigurationFinders).IsNotNull();
            AddConfigurationFinders(mappingConfigurationFinders);
        }
        

        private void AddConfigurationFinders(IEnumerable<MappingConfigurationTypesFinder> mappingConfigurationFinders)
        {
            foreach (var configurationTypesFinder in mappingConfigurationFinders)
            {
                AddConfigurationTypeFinder(configurationTypesFinder);
            }

            if (!_finders.ContainsKey(typeof(DefaultMappingConfigurationTypesFinder)))
            {
                AddConfigurationTypeFinder(new DefaultMappingConfigurationTypesFinder());
            }
        }

        public DataContextFactory(IEnumerable<MappingConfigurationTypesFinder> mappingConfigurationFinders, 
                                  string nameOrConnectionString)
            : this(mappingConfigurationFinders)
        {
            Guard.That(nameOrConnectionString).IsNotNullOrEmpty();
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void AddConfigurationTypeFinder(MappingConfigurationTypesFinder configurationTypesFinder)
        {
            Guard.That(configurationTypesFinder).IsNotNull();
            AddConfigurationTypeFinder(configurationTypesFinder.GetType(), () => configurationTypesFinder);
        }

        public void AddConfigurationTypeFinder<TConfigurationTypeFinder>()
           where TConfigurationTypeFinder : MappingConfigurationTypesFinder, new()
        {
            AddConfigurationTypeFinder(typeof(TConfigurationTypeFinder), () => new TConfigurationTypeFinder());
        }

        private void AddConfigurationTypeFinder(Type mappingConfigurationTypeFinder, 
                                                Func<MappingConfigurationTypesFinder> conventionCreator)
        {
            if (!_finders.ContainsKey(mappingConfigurationTypeFinder))
            {
                _finders.Add(mappingConfigurationTypeFinder, conventionCreator());
            }
        }

        public void RemoveConvention<TConfigurationTypeFinder>() 
            where TConfigurationTypeFinder : MappingConfigurationTypesFinder
        {
            var conventionType = typeof(TConfigurationTypeFinder);
            if (_finders.ContainsKey(conventionType))
            {
                _finders.Remove(conventionType);
            }
        }

        public DataContext Create()
        {
            return new DataContext(MappingConfigurationTypes, _nameOrConnectionString);
        }
    }
}