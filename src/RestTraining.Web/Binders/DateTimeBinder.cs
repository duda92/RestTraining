using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace RestTraining.Web.Binders
{
    public class DateTimeBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "BoundedPeriodId")
            {
                var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                if (!string.IsNullOrEmpty(displayFormat) && value != null)
                {
                    DateTime date;
                    displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                    // use the format specified in the DisplayFormat attribute to parse the date
                    if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        propertyDescriptor.SetValue(bindingContext.Model, date);
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            string.Format("{0} is an invalid date format", value.AttemptedValue)
                        );
                    }
                    return;
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}