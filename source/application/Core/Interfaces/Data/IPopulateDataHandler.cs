namespace Intrigma.DonorSpace.Core.Interfaces.Data
{
    public interface IPopulateDataHandler<in TContext>
    {
        void Populate(TContext context);
    }
}