using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using Intrigma.DonorSpace.Acceptance.Configuration;
using NUnit.Framework;

namespace Intrigma.DonorSpace.ExecutableSpecifications
{
    /// <summary>
    /// This class has methods that run before and after each test run
    /// It configures and disposes the Inversion of Control container
    /// </summary>
    [SetUpFixture]
    public class ExecutableSpecificationsConfigurator
    {
        [SetUp]
        public void SetUpTestEnvironment()
        {
            BddifyConfiguration.InitializeBddify(new ExecutableSpecificationsHtmlReportConfig());
            BuilderSetup.SetDefaultPropertyNamer(new PersistableEntityPropertyNamer(new ReflectionUtil()));
            TestConfigurator.BuildContainer();
        }

        [TearDown]
        public void TearDownTestEnvironment()
        {
            TestConfigurator.DisposeContainer();
        }
    }
}