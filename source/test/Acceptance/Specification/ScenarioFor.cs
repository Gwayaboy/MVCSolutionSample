using System;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    public abstract class ScenarioFor<TScenarioContext, TStory> : Scenario
        where TScenarioContext : class
    {
        public TScenarioContext SUT { get; protected set; }
        
        public override Type Story
        {
            get { return typeof (TStory); }
        }

        protected ScenarioFor(int scenarioNumber, string scenarioTitle)
        {
            Guard.That(() => scenarioNumber).IsGreaterThan(0);
            Guard.That(() => scenarioTitle).IsNotNullOrEmpty();

            Number = scenarioNumber;
            Title = scenarioTitle;
        }

        public override void InitialiseSystemUnderTest()
        {
            SUT = ScopedContainer.Resolve<TScenarioContext>();
        }
        
        protected override string BuildTitle()
        {
            return string.Format("Scenario {0}: {1}{2}", Number.ToString("00"), Title, Category);
        }
    }
}