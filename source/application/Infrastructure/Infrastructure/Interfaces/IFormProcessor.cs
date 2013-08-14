using Panzea.DonorSpace.Infrastructure.ApplicationMessage;

namespace Panzea.DonorSpace.Infrastructure.Interfaces
{
    public interface IFormProcessor
    {
        ExecutionResult Execute<TForm>(TForm form)
            where TForm : class;
    }
}