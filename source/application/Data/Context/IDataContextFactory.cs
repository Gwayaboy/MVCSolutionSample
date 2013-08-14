namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context
{
    public interface IDataContextFactory
    {
        DataContext Create();
    }
}