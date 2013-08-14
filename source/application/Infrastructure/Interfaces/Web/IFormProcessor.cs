using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;

namespace Intrigma.DonorSpace.Infrastructure.Interfaces.Web
{
    public interface IFormProcessor
    {
        ExecutionResult Execute<TForm>(TForm form)
            where TForm : class;
    }
}