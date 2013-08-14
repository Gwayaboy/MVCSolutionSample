using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    public class ScenarioCategory : Enumeration<ScenarioCategory, int>
    {
        public static ScenarioCategory Normal = new ScenarioCategory(1,"");
        public static ScenarioCategory NotImplementedSubcutaneously = new ScenarioCategory(2, " - COVERED BY BROWSER TESTING");
        
        public ScenarioCategory(int value, string displayName) : base(value, displayName) { }
    }
}