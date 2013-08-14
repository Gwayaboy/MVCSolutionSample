using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Population
{
    public abstract class DataContextPopulator : IPopulate<DataContext>
    {
        public virtual int ExecutionPriority { get { return 0; } }

        public abstract void Populate(DataContext context);
    }
}