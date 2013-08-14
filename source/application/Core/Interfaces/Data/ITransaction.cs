using System;

namespace Intrigma.DonorSpace.Core.Interfaces.Data
{
    public interface ITransaction : IDisposable
    {
        void Commit();
    }
}