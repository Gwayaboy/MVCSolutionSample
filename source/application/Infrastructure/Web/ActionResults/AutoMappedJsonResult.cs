using System;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Infrastructure.Web.ActionResults
{
    public class AutoMappedJsonResult : JsonResult, IAutoMapViewModel
    {
        public object BuildViewModel()
        {
            throw new NotImplementedException();
        }
    }
}