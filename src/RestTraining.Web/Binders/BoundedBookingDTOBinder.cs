using System;
using System.Linq;
using System.Web.Mvc;

namespace RestTraining.Web.Binders
{
    public class BoundedBookingDTOBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "BoundedPeriodId")
            {
                string periodIdString = bindingContext.ValueProvider.GetValue("BoundedPeriodId").AttemptedValue;
                int periodId = 0;
                int.TryParse(periodIdString, out periodId);
                propertyDescriptor.SetValue(bindingContext.Model, periodId);
                return;
            }
            if (propertyDescriptor.Name == "HotelNumberId")
            {
                string periodIdString = bindingContext.ValueProvider.GetValue("HotelNumberId").AttemptedValue;
                int periodId = 0;
                int.TryParse(periodIdString, out periodId);
                propertyDescriptor.SetValue(bindingContext.Model, periodId);
                return;
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}