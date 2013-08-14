using System;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Infrastructure.Web.ActionResults
{
    public class AutoMappedViewResult : ViewResult, IAutoMapViewModel
    {
        private readonly IModelMapper _modelMapper;

        public AutoMappedViewResult(IModelMapper modelMapper,
            object sourceEntity, 
            Type viewModelType)
        {
            _modelMapper = modelMapper;
            SourceEntity = sourceEntity;
            ViewModelType = viewModelType;
        }

        public object SourceEntity { get; private set; }
        public Type ViewModelType { get; private set; }

        public AutoMappedViewResult(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ViewData.Model = BuildViewModel();

            base.ExecuteResult(context);
        }

        public object BuildViewModel()
        {
            return _modelMapper.Map(SourceEntity, ViewModelType);
        }
    }
}