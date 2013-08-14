namespace Intrigma.DonorSpace.Core.Interfaces.Data
{
    public interface IUnitOfWork 
    {
        ITransaction BeginTransaction();
    }
}