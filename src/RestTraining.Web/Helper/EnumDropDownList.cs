﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace RestTraining.Web.Helper
{
    public static class EnumDropDownList
    {
        public static HtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> modelExpression, object model = null, string firstElement = null, bool isDisabled = false)
        {
            var typeOfProperty = modelExpression.ReturnType;
            if (!typeOfProperty.IsEnum)
                throw new ArgumentException(string.Format("Type {0} is not an enum", typeOfProperty));

            var enumValues = new SelectList(Enum.GetValues(typeOfProperty), model);
            if (isDisabled)
                return firstElement != null ? htmlHelper.DropDownListFor(modelExpression, enumValues, firstElement, new { disabled = "disabled" }) : htmlHelper.DropDownListFor(modelExpression, enumValues, new { disabled = "disabled" });
            else
                return firstElement != null ? htmlHelper.DropDownListFor(modelExpression, enumValues, firstElement) : htmlHelper.DropDownListFor(modelExpression, enumValues);
        }
    }
}