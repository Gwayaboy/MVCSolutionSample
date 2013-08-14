using System;
using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IModelMapper
    {
        ICommand MapFormToCommand<TForm>(TForm form);
        object Map(object source, Type viewModelType);
        TViewModel MapViewModel<TViewModel>(object source);
    }   
}