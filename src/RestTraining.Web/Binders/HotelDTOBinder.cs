using RestTraining.Web.Helper;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RestTraining.Web.Binders
{
    public class HotelDTOBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "Image")
            {
                byte[] imageBytes = null;
                if (controllerContext.HttpContext.Request.Files.Count == 1 && controllerContext.HttpContext.Request.Files[0].FileName != string.Empty)
                {
                    var stream = controllerContext.HttpContext.Request.Files[0].InputStream;
                    imageBytes = ImageHelper.GetBytes(stream);
                    if (!ImageHelper.IsValidImage(imageBytes))
                    {
                        bindingContext.ModelState.AddModelError(propertyDescriptor.Name, "Uploaded image is invalid");
                        propertyDescriptor.SetValue(bindingContext.Model, null);
                    }
                    else
                    {
                        propertyDescriptor.SetValue(bindingContext.Model, imageBytes);
                    } 
                    return;
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}