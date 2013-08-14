using System;
using Intrigma.DonorSpace.Acceptance.Helper;
using Intrigma.DonorSpace.Acceptance.Specification.Resolution;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    public abstract class ScenarioFor<TScenarioContext, TStory> : Specification,IScenario
        where TScenarioContext : class
    {
        private IScope _scope;

        public TScenarioContext SUT { get;  protected set; }
        public int Number { get; protected set; }
        protected IDatabase Database { get;  set; }

        public override Type Story
        {
            get { return typeof(TStory); }
        }

        protected ScenarioFor(int scenarioNumber, string scenarioTitle)
        {
            Guard.That(() => scenarioNumber).IsGreaterThan(0);
            Guard.That(() => scenarioTitle).IsNotNullOrEmpty();

            Number = scenarioNumber;
            Title = scenarioTitle;
        }

        public override void EstablishContext()
        {
            _scope = ScopeProvider();
            SUT = _scope.Resolve<TScenarioContext>();
            Database = _scope.Resolve<IDatabase>();
        }

        public override void Setup()
        {
            Database.ResetData();
        }
        
        public override void TearDown()
        {
            _scope.Dispose();
        }

        protected override string BuildTitle()
        {
            return string.Format("Scenario {0}: {1}{2}", Number.ToString("00"), Title, Category);
        }
    }
}