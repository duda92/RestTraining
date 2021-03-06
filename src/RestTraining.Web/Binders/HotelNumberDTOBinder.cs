﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestTraining.Common.DTO;

namespace RestTraining.Web.Binders
{
    public class HotelNumberDTOBinder : BinderBase
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
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }

    }
}