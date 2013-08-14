using System;
using System.Linq;
using Intrigma.DonorSpace.Acceptance.Specification.Resolution;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners;

namespace Intrigma.DonorSpace.Acceptance.Configuration
{
    public class SpecStoryMetaDataScanner : IStoryMetaDataScanner
    {
        public virtual StoryMetaData Scan(object testObject, Type explicityStoryType = null)
        {
            var specification = testObject as ISpecification;
            if (specification == null)
                return null;

            var storyAttribute = GetStoryAttribute(specification.Story) ?? CreateStoryAttributeWithSpecificationTitle(specification);
           
            return new StoryMetaData(specification.Story, storyAttribute);
        }

        private static StoryAttribute GetStoryAttribute(Type candidateStoryType)
        {
            return (StoryAttribute)candidateStoryType.GetCustomAttributes(typeof(StoryAttribute), true).FirstOrDefault();
        }

        private StoryAttribute CreateStoryAttributeWithSpecificationTitle(ISpecification specification)
        {
            const string suffix = "Specification";
            var title = specification.Story.Name;
            if (title.EndsWith(suffix))
                title = title.Remove(title.Length - suffix.Length, suffix.Length);
            return new StoryAttribute { Title = title };
        }
    }
}