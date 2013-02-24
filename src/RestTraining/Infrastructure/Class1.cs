

using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using IModelBinder = System.Web.Http.ModelBinding.IModelBinder;
using ModelBindingContext = System.Web.Http.ModelBinding.ModelBindingContext;

namespace RestTraining.Api.Infrastructure
{
    public class MyRequestModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            return null;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {

            return true;
        }
    }

    public class CustomPersonModelBinderProvider : ModelBinderProvider
    {
        private MyRequestModelBinder _customPersonModelBinder = new MyRequestModelBinder();

        public override System.Web.Http.ModelBinding.IModelBinder GetBinder(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            return new MyRequestModelBinder();
        }
    }
}