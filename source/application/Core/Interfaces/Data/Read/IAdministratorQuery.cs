using Intrigma.DonorSpace.Core.Domain;

namespace Intrigma.DonorSpace.Core.Interfaces.Data.Read
{
    public interface IAdministratorQuery : IQuery<Administrator>
    {
        Administrator FindByUserName(string userName);
    }
}