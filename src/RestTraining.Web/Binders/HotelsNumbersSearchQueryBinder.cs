using System.Web.Mvc;

namespace RestTraining.Web.Binders
{
    public class HotelsNumbersSearchQueryBinder : BinderBase
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "IncludeItems")
            {
                if (BindIncludeItems(controllerContext, bindingContext, propertyDescriptor))
                {
                    return;
                }
            }
            if (propertyDescriptor.Name == "HotelsAttractions")
            {
                if (BindHotelsAttractionsItems(controllerContext, bindingContext, propertyDescriptor))
                {
                    return;
                }
            }
            if (propertyDescriptor.Name == "WindowViews")
            {
                if (base.BindWindowViews(controllerContext, bindingContext, propertyDescriptor))
                {
                    return;
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}