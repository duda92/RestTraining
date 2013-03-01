using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestTraining.Api;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Domain;

namespace RestTraining.Web.Controllers
{
    public partial class HotelNumbersController : ControllerBase
    {
        public const string Resource = "/api/Hotels/{0}/HotelNumbers/";

        public virtual ActionResult Index(int hotelId)
        {
            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(Resource, hotelId));
            return View(hotelNumbers);
        }

        [HttpGet]
        public virtual ActionResult Create(int hotelId)
        {
            return View(MVC.HotelNumbers.Views.EditOrCreate, new HotelNumberDTO());
        }
        
        [HttpPost]
        public virtual ActionResult Create(int hotelId, HotelNumberDTO hotelNumber)
        {
            if (!ModelState.IsValid)
                return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
            JsonRequestExecutor.ExecutePost(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }
        
        [HttpGet]
        public virtual ActionResult Edit(int hotelId, int id)
        {
            var hotelNumber = JsonRequestExecutor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
            return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, HotelNumberDTO hotelNumber)
        {
            if (!ModelState.IsValid)
                return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
            JsonRequestExecutor.ExecutePut(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }

        public virtual ActionResult Delete(int hotelId, int hotelNumberId)
        {
            JsonRequestExecutor.ExecuteDelete<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, hotelNumberId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }
    }

    public class HotelNumberDTOBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var hotelNumberDTO = new HotelNumberDTO();
            SetId(bindingContext, hotelNumberDTO);
            SetWindowViewTypes(bindingContext, hotelNumberDTO);
            SetHotelNumberType(bindingContext, hotelNumberDTO);
            SetIncludeItems(bindingContext, hotelNumberDTO);
            return hotelNumberDTO;
        }

        private void SetIncludeItems(ModelBindingContext bindingContext, HotelNumberDTO hotelNumberDTO)
        {
            try
            {
                ValueProviderResult includeItemCountParam1VPR = bindingContext.ValueProvider.GetValue("IncludeItem.Count");
                var includeItemCountParam1 = includeItemCountParam1VPR != null
                                                 ? includeItemCountParam1VPR.AttemptedValue.Split(',')
                                                 : new string[0];
                ValueProviderResult includeItemCountParam2VPR = bindingContext.ValueProvider.GetValue("IncludeItems.item.Count");
                var includeItemCountParam2 = includeItemCountParam2VPR != null
                                                 ? includeItemCountParam2VPR.AttemptedValue.Split(',')
                                                 : new string[0];
                ValueProviderResult includeItemTypeParam1VPR = bindingContext.ValueProvider.GetValue("IncludeItem.IncludeItemType");
                var includeItemTypeParam1 = includeItemTypeParam1VPR != null
                                                ? includeItemTypeParam1VPR.AttemptedValue.Split(',')
                                                : new string[0];
                ValueProviderResult includeItemTypeParam2VPR =
                    bindingContext.ValueProvider.GetValue("IncludeItems.item.IncludeItemType");
                var includeItemTypeParam2 = includeItemTypeParam2VPR != null
                                                ? includeItemTypeParam2VPR.AttemptedValue.Split(',')
                                                : new string[0];
                var includeItems = new List<IncludedItemDTO>();
                var includeItemCountParamsList = includeItemCountParam1.Concat(includeItemCountParam2).ToArray();
                var includeItemTypeParamsList = includeItemTypeParam1.Concat(includeItemTypeParam2).ToArray();
                for (int i = 0; i < includeItemTypeParamsList.Count(); i++)
                    includeItems.Add(new IncludedItemDTO
                    {
                        Count = Int32.Parse(includeItemCountParamsList[i]),
                        IncludeItemType =
                            (IncludeItemType)
                            Enum.Parse(typeof(IncludeItemType), includeItemTypeParamsList[i]),
                    });
                hotelNumberDTO.IncludeItems = includeItems;
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError("cannot bind include items", e);
            }
        }

        private void SetHotelNumberType(ModelBindingContext bindingContext, HotelNumberDTO hotelNumberDTO)
        {
            try
            {
                var hotelNumberTypeParam = bindingContext.ValueProvider.GetValue("HotelNumberType").AttemptedValue;
                var hotelNumberType = (HotelNumberType)Enum.Parse(typeof(HotelNumberType), hotelNumberTypeParam);
                hotelNumberDTO.HotelNumberType = hotelNumberType;
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError("cannot bind HotelNumberType", e);
            }
        }

        private void SetWindowViewTypes(ModelBindingContext bindingContext, HotelNumberDTO hotelNumberDTO)
        {

            try
            {
                ValueProviderResult viewTypesParamVPR = bindingContext.ValueProvider.GetValue("WindowViews");
                var viewTypesParam = viewTypesParamVPR != null ? viewTypesParamVPR.AttemptedValue.Split(',') : new string[0];
                var windowViews =
                    viewTypesParam.Select(viewType => (WindowViewType)Enum.Parse(typeof(WindowViewType), viewType)).ToList();
                hotelNumberDTO.WindowViews = windowViews;
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError("cannot bind WindowViewTypes", e);
            }
            
        }

        private void SetId(ModelBindingContext bindingContext, HotelNumberDTO hotelNumberDTO)
        {
            try
            {
                var idParam = bindingContext.ValueProvider.GetValue("id").AttemptedValue;
                var id = Int32.Parse(idParam);
                hotelNumberDTO.Id = id;
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError("cannot bind id", e);
            }
            
        }
    }  
}

//public class IncludedItemsListBinder : IModelBinder
//{
//    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
//    {
//        var includeItems = new List<IncludedItemDTO>();
//        try
//        {
//            var includeItemCountParam = bindingContext.ValueProvider.GetValue("IncludeItem.item.Count").AttemptedValue;
//            var includeItemTypeParam = bindingContext.ValueProvider.GetValue("IncludeItem.item.IncludeItemType").AttemptedValue;

//            var includeItemCountParamsList = includeItemCountParam.Split(',');
//            var includeItemTypeParamsList = includeItemTypeParam.Split(',');
//            for (int i = 0; i < includeItemTypeParamsList.Count(); i++)
//                includeItems.Add(new IncludedItemDTO
//                {
//                    Count = Int32.Parse(includeItemCountParamsList[i]),
//                    IncludeItemType = (IncludeItemType)Int32.Parse(includeItemTypeParamsList[i]),
//                });
//            return includeItems;
//        }
//        catch (Exception e)
//        {
//            bindingContext.ModelState.AddModelError("", e);
//            return includeItems;
//        }
//    }
//}