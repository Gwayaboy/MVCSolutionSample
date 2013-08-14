using System;
using System.Reflection;
using Autofac;
using AutofacContrib.NSubstitute;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    public abstract class SpecificationFor<T> : Specification
    {
        private readonly AutoSubstitute _autoSubstitute;
        public T SUT { get; set; }

        protected const int OnlyOnce = 1;

        protected SpecificationFor()
        {
            _autoSubstitute = CreateContainer();
            SUT = _autoSubstitute.Resolve<T>();
        }

       

        public TSubstitute SubstituteFor<TSubstitute>() where TSubstitute : class
        {
            if (HasNoSubstituteForConcreteType<TSubstitute>())
            {
                return _autoSubstitute.SubstituteFor<TSubstitute>();
            }
            return _autoSubstitute.ResolveAndSubstituteFor<TSubstitute>();
        }
        
        public override Type Story
        {
            get { return typeof(T); }
        }

        private bool HasNoSubstituteForConcreteType<TSubstitute>() where TSubstitute : class
        {
            return typeof(TSubstitute).IsConcrete() && (_autoSubstitute.Resolve<TSubstitute>().GetType() == typeof(TSubstitute));
        }



        private static AutoSubstitute CreateContainer(Action<ContainerBuilder> containerRegistrationAction = null)
        {
            Action<ContainerBuilder> autofacCustomisation = c =>
                {

                    if (containerRegistrationAction != null) containerRegistrationAction(c);
                    c.RegisterType<T>()
                     .FindConstructorsWith(
                         t => t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                     .PropertiesAutowired();
                };
            return new AutoSubstitute(autofacCustomisation);
        }
    }
}
