using System;
using Intrigma.DonorSpace.Acceptance.Helper;
using Intrigma.DonorSpace.Acceptance.Specification.Resolution;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    public abstract class Scenario : Specification, IScenario
    {
        public int Number { get; protected set; }
        public IDatabase Database { get; protected set; }
        protected IScopedContainer ScopedContainer { get; private set; }

        internal static Func<IScopedContainer> ScopedContainerProvider { get; set; }

        public override void EstablishContext()
        {
            ScopedContainer = ScopedContainerProvider();
            Database = ScopedContainer.Resolve<IDatabase>();
            InitialiseSystemUnderTest();
        }

        public abstract void InitialiseSystemUnderTest();

        public override void Setup()
        {
            Database.ResetData();
        }

        public override void TearDown()
        {
            ScopedContainer.Dispose();
        }

    }
}