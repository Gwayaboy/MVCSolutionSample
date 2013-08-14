using System;
using Humanizer;
using Intrigma.DonorSpace.Acceptance.Specification.Resolution;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    [TestFixture]
    public abstract class Specification  : ISpecification
    {
        
        [Test]
        public virtual void Run()
        {
            var title = BuildTitle();
            this.BDDfy(title, Category.DisplayName);
        }

        protected virtual string BuildTitle()
        {
            return Title ?? GetType().Name.Humanize(LetterCasing.Sentence);
        }


        // BDDfy methods
        public virtual void EstablishContext() { }
        public virtual void Setup() { }
        public virtual void TearDown() { }

        public virtual Type Story { get { return GetType(); } }
        public virtual string Title { get; set; }
        public virtual ScenarioCategory Category
        {
            get { return ScenarioCategory.Normal; }
        }

        public static Func<IScope> ScopeProvider{ get; set; }
    }
}