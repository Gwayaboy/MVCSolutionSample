namespace Intrigma.DonorSpace.Core.Interfaces.Data
{
    public interface IPopulate<in TContext>
        where TContext : class
    {
        int ExecutionPriority { get; }
        void Populate(TContext context);
    }
}