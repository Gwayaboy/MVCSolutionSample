using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Intrigma.DonorSpace.Acceptance.Helper;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Infrastructure.Configuration;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;
using Intrigma.DonorSpace.Infrastructure.Web;
using Module = Autofac.Module;

namespace Intrigma.DonorSpace.Acceptance.Modules
{
    public class ScenarioModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            SetControllerScenarios(builder);
        }

        private void SetControllerScenarios(ContainerBuilder builder)
        {
            var controllerScenarioTypes =
                AssemblyScanner
                    .GetExportedTypesFromAssemblyContaining<NotImplementedSubcutaneouslyScenario>()
                    .GetConcreteTypesImplementingOpenGenericInterfaceType(typeof (IControllerScenario<>))
                    .Where(result => result.ConcreteType.IsGenericTypeDefinition);

            foreach (var result in controllerScenarioTypes)
            {
                builder
                    .RegisterGeneric(result.ConcreteType)
                    .AsSelf()
                    .OnActivating(e =>
                        {
                            AssignControllerInstance(e);
                            AssignFormValidatorFactoryWhenPostControllerScenario(e);
                        });
            }
        }

        private void AssignFormValidatorFactoryWhenPostControllerScenario(IActivatingEventArgs<object> activatingArgs)
        {

            if (IsPostControllerScenario(activatingArgs.Instance))
            {
                var postControllerScenarioInstance = activatingArgs.Instance;
                
                var formValidatorFactoryProperty =
                    postControllerScenarioInstance
                        .GetType()
                        .GetProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                        .Single(p => p.PropertyType == typeof (IFormValidatorFactory));

                formValidatorFactoryProperty
                    .SetValue(postControllerScenarioInstance,
                              activatingArgs.Context.Resolve<IFormValidatorFactory>());
            }
        }

        private static bool IsPostControllerScenario(object candidateInstance)
        {
            if (candidateInstance == null) return false;


            var candidateType = candidateInstance.GetType();
            return candidateType.IsGenericType &&
                   candidateType.GetGenericTypeDefinition() == typeof (PostControllerScenario<,>) &&
                   candidateType.GetGenericArguments().Any();
        }

        private static void AssignControllerInstance(IActivatingEventArgs<object> activatingArgs)
        {
            var controllerScenarioInstance = activatingArgs.Instance;

            var controllerProperty =
                controllerScenarioInstance
                    .GetType()
                    .GetProperties()
                    .Single(x => x.PropertyType.IsConcreteTypeOf<BaseController>());

            var controller = activatingArgs.Context.Resolve(controllerProperty.PropertyType);

            controllerProperty.SetValue(controllerScenarioInstance, controller);
        }
    }
}