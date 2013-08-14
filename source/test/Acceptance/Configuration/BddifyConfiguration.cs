using Intrigma.DonorSpace.Acceptance.Specification;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Processors.HtmlReporter;

namespace Intrigma.DonorSpace.Acceptance.Configuration
{
    public static class BddifyConfiguration
    {
        public static void InitializeBddify(DefaultHtmlReportConfiguration reportConfiguration)
        {
            Configurator.Processors.TestRunner.RunsOn(s => s.Category != ScenarioCategory.NotImplementedSubcutaneously.DisplayName);
            Configurator.Scanners.StoryMetaDataScanner = () => new SpecStoryMetaDataScanner();
            Configurator.BatchProcessors.HtmlReport.Disable();
            Configurator.BatchProcessors.Add(new HtmlReporter(reportConfiguration));
        }
    }
}