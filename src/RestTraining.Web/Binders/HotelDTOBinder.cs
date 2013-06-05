using System;
using System.Collections.Generic;
using System.Linq;
using RestTraining.Common.DTO;
using RestTraining.Web.Helper;
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
            if (propertyDescriptor.Name == "HotelsAttractions")
            {
                try
                {
                    ValueProviderResult hotelsAttractionCountParamVPR = bindingContext.ValueProvider.GetValue("HotelsAttraction.Count");
                    var hotelsAttractionCountParamsList = hotelsAttractionCountParamVPR != null
                                                     ? hotelsAttractionCountParamVPR.AttemptedValue.Split(',')
                                                     : new string[0];
                    ValueProviderResult hotelsAttractionTypeParamsListVPR =
                        bindingContext.ValueProvider.GetValue("HotelsAttraction.HotelsAttractionType");
                    var hotelsAttractionTypeParamsList = hotelsAttractionTypeParamsListVPR != null
                                                    ? hotelsAttractionTypeParamsListVPR.AttemptedValue.Split(',')
                                                    : new string[0];
                    
                    var hotelsAttractions = new List<HotelsAttractionDTO>();
                     for (int i = 0; i < hotelsAttractionTypeParamsList.Count(); i++)
                        hotelsAttractions.Add(new HotelsAttractionDTO
                        {
                            Count = Int32.Parse(hotelsAttractionCountParamsList[i]),
                            HotelsAttractionType = 
                                (HotelsAttractionTypeDTO)
                                Enum.Parse(typeof(HotelsAttractionTypeDTO), hotelsAttractionTypeParamsList[i]),
                        });
                    propertyDescriptor.SetValue(bindingContext.Model, hotelsAttractions);
                    return;
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError("cannot bind hotel's attractions", e);
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}