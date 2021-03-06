﻿using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace RestTraining.Web.Helper
{
    public static class MvcHtmlHelpers
    {
        public static HtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> modelExpression, object model = null, string firstElement = null)
        {
            var typeOfProperty = modelExpression.ReturnType;
            if (!typeOfProperty.IsEnum)
                throw new ArgumentException(string.Format("Type {0} is not an enum", typeOfProperty));

            var enumValues = new SelectList(Enum.GetValues(typeOfProperty), model);
            return firstElement != null ? htmlHelper.DropDownListFor(modelExpression, enumValues, firstElement) : htmlHelper.DropDownListFor(modelExpression, enumValues);
        }

        //public static HtmlString DateRangePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> beginDateExpression, Expression<Func<TModel, TProperty>> endDateExpression)
        //{
            
        //}
    }
}