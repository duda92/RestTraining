using System;
using System.Linq;
using System.Web.Mvc;

namespace RestTraining.Web.Binders
{
    public class BookingDTOBinder : DefaultModelBinder
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
            if (propertyDescriptor.PropertyType == typeof(DateTime))
            {
                string periodIdString = bindingContext.ValueProvider.GetValue(propertyDescriptor.Name).AttemptedValue;
                DateTime date;
                DateTime.TryParse(periodIdString, out date);
                propertyDescriptor.SetValue(bindingContext.Model, date);
                return;
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}