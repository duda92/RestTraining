// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace RestTraining.Web.Controllers
{
    public partial class BoundedBookingController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BoundedBookingController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BoundedBookingController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Create()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Delete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BoundedBookingController Actions { get { return MVC.BoundedBooking; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "BoundedBooking";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "BoundedBooking";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Create = "Create";
            public readonly string Edit = "Edit";
            public readonly string Delete = "Delete";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
        }


        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string hotelId = "hotelId";
            public readonly string boundedBooking = "boundedBooking";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string hotelId = "hotelId";
            public readonly string id = "id";
            public readonly string boundedBooking = "boundedBooking";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string hotelId = "hotelId";
            public readonly string id = "id";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string BookingDeleted = "BookingDeleted";
                public readonly string BookingEdited = "BookingEdited";
                public readonly string EditOrCreate = "EditOrCreate";
                public readonly string Index = "Index";
            }
            public readonly string BookingDeleted = "~/Views/BoundedBooking/BookingDeleted.cshtml";
            public readonly string BookingEdited = "~/Views/BoundedBooking/BookingEdited.cshtml";
            public readonly string EditOrCreate = "~/Views/BoundedBooking/EditOrCreate.cshtml";
            public readonly string Index = "~/Views/BoundedBooking/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_BoundedBookingController : RestTraining.Web.Controllers.BoundedBookingController
    {
        public T4MVC_BoundedBookingController() : base(Dummy.Instance) { }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int hotelId);

        public override System.Web.Mvc.ActionResult Create(int hotelId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "hotelId", hotelId);
            CreateOverride(callInfo, hotelId);
            return callInfo;
        }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int hotelId, RestTraining.Api.DTO.BoundedBookingDTO boundedBooking);

        public override System.Web.Mvc.ActionResult Create(int hotelId, RestTraining.Api.DTO.BoundedBookingDTO boundedBooking)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "hotelId", hotelId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "boundedBooking", boundedBooking);
            CreateOverride(callInfo, hotelId, boundedBooking);
            return callInfo;
        }

        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int hotelId, int id);

        public override System.Web.Mvc.ActionResult Edit(int hotelId, int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "hotelId", hotelId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, hotelId, id);
            return callInfo;
        }

        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int hotelId, RestTraining.Api.DTO.BoundedBookingDTO boundedBooking);

        public override System.Web.Mvc.ActionResult Edit(int hotelId, RestTraining.Api.DTO.BoundedBookingDTO boundedBooking)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "hotelId", hotelId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "boundedBooking", boundedBooking);
            EditOverride(callInfo, hotelId, boundedBooking);
            return callInfo;
        }

        partial void DeleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int hotelId, int id);

        public override System.Web.Mvc.ActionResult Delete(int hotelId, int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "hotelId", hotelId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteOverride(callInfo, hotelId, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
