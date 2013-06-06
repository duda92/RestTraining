using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestTraining.Common.DTO;
using RestTraining.Web.Helper;

namespace RestTraining.Web.Binders
{
   public class BinderBase : DefaultModelBinder
    {
       public bool BindIncludeItems(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            try
            {
                var paramsPrefixValiants = new List<string>
                                               {
                                                   "IncludeItems.IncludeItem",
                                                   "IncludeItems.item",
                                                   "Query.IncludeItems.IncludeItem",
                                                   "Query.IncludeItems.item"
                                               };

                var includedItems = new List<IncludedItemDTO>();
                
                foreach (var paramsPrefixValiant in paramsPrefixValiants)
                {
                    var countParams = bindingContext.ValueProvider.GetValue(string.Format("{0}.{1}", paramsPrefixValiant, "Count"));
                    var countParamsList = countParams != null  ? countParams.AttemptedValue.Split(',') : new string[0];

                    var typeParams = bindingContext.ValueProvider.GetValue(string.Format("{0}.{1}", paramsPrefixValiant, "IncludeItemType"));
                    var typeParamsList = typeParams != null ? typeParams.AttemptedValue.Split(',')  : new string[0];

                    for (var i = 0; i < typeParamsList.Count(); i++)
                    {
                        var includeItem = new IncludedItemDTO
                                              {
                                                  Count = Int32.Parse(countParamsList[i]),
                                                  IncludeItemType =
                                                      (IncludeItemTypeDTO)
                                                      Enum.Parse(typeof(IncludeItemTypeDTO), typeParamsList[i]),
                                              };
                        includedItems.Add(includeItem);
                    }
                }
                propertyDescriptor.SetValue(bindingContext.Model, includedItems);
                return true;
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError("cannot bind include items", e);
                return false;
            }
        }
    
       public bool BindHotelsAttractionsItems(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
       {
           try
           {
               var paramsPrefixValiants = new List<string>
                                               {
                                                   "HotelsAttractions.HotelsAttraction",
                                                   "HotelsAttractions.item",
                                                   "Query.HotelsAttractions.HotelsAttraction",
                                                   "Query.HotelsAttractions.item"
                                               };

               var hotelsAttractions = new List<HotelsAttractionDTO>();

               foreach (var paramsPrefixValiant in paramsPrefixValiants)
               {
                   var countParams = bindingContext.ValueProvider.GetValue(string.Format("{0}.{1}", paramsPrefixValiant, "Count"));
                   var countParamsList = countParams != null ? countParams.AttemptedValue.Split(',') : new string[0];

                   var typeParams = bindingContext.ValueProvider.GetValue(string.Format("{0}.{1}", paramsPrefixValiant, "HotelsAttractionType"));
                   var typeParamsList = typeParams != null ? typeParams.AttemptedValue.Split(',') : new string[0];

                   for (var i = 0; i < typeParamsList.Count(); i++)
                   {
                       var hotelsAttraction = new HotelsAttractionDTO
                       {
                           Count = Int32.Parse(countParamsList[i]),
                           HotelsAttractionType = (HotelsAttractionTypeDTO) Enum.Parse(typeof(HotelsAttractionTypeDTO), typeParamsList[i]),
                       };
                       hotelsAttractions.Add(hotelsAttraction);
                   }
               }
               propertyDescriptor.SetValue(bindingContext.Model, hotelsAttractions);
               return true; 
           }
           catch (Exception e)
           {
               bindingContext.ModelState.AddModelError("cannot bind hotel's attractions", e);
               return false;
           }
       }

       public void BindImage(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
       {
           if (controllerContext.HttpContext.Request.Files.Count != 1 ||
               controllerContext.HttpContext.Request.Files[0].FileName == string.Empty) return;
           var stream = controllerContext.HttpContext.Request.Files[0].InputStream;
           var imageBytes = ImageHelper.GetBytes(stream);
           if (!ImageHelper.IsValidImage(imageBytes))
           {
               bindingContext.ModelState.AddModelError(propertyDescriptor.Name, "Uploaded image is invalid");
               propertyDescriptor.SetValue(bindingContext.Model, null);
           }
           else
           {
               propertyDescriptor.SetValue(bindingContext.Model, imageBytes);
           }
       }
    }
}