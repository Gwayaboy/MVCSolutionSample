using System.Collections.Generic;
using System.Linq;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Population
{
    public class PopulateDataContextHandler : IPopulateDataHandler<DataContext>
    {
        private readonly IEnumerable<IPopulate<DataContext>> _dataContextPopulators;

        public PopulateDataContextHandler(IEnumerable<IPopulate<DataContext>> dataContextPopulators )
        {
            _dataContextPopulators = dataContextPopulators.OrderBy(x => x.ExecutionPriority);
        }

        public void Populate(DataContext context)
        {
            foreach (var dataContextPopulator in _dataContextPopulators)
            {
                dataContextPopulator.Populate(context);
            }
        }
    }
}