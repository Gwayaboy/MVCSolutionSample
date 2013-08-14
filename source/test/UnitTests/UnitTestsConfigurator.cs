using Intrigma.DonorSpace.Acceptance.Configuration;
using NUnit.Framework;

namespace Intrigma.DonorSpace.UnitTests
{
    /// <summary>
    /// This class has methods that run before and after each test run
    /// It configures and disposes the Inversion of Control container
    /// </summary>
    [SetUpFixture]
    public class UnitTestsConfigurator
    {
        [SetUp]
        public void SetUpTestEnvironment()
        {
            BddifyConfiguration.InitializeBddify(new UnitTestsSpecificationsHtmlReportConfig());
        }

        
    }
}