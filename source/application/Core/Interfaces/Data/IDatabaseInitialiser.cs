namespace Intrigma.DonorSpace.Core.Interfaces.Data
{
    public interface IDatabaseInitialiser
    {
        void Initialise(bool forceReinitialisation = false);
    }
}