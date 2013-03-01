using System;
using System.Web.Mvc;

namespace RestTraining.Web
{
    public enum ControllerActionType
    {
        Undefined,
        Edit,
        Create
    }

    public class ViewUtils
    {
        
        public const string ControllerActionTypeKey = "controller_action_key";
        public const string InvalidBoundedPeriodKey = "invalid_bounded_period_key";
        public const string HotelIdKey = "hotel_id_key";
        public const string ApiServiceNotResponse = "api_service_not_response";
        public const string ResourceNotFoundKey = "resource_not_found_key";
        public static string FreeBookingDatesConflictKey = "free_booking_dates_conflict_key";
    }

    public class ViewDataProviderForView
    {
        private readonly WebViewPage _view;

        public ViewDataProviderForView(WebViewPage view)
        {
            _view = view;
        }

        public int HotelId
        {
            get
            {
                var routeHotelId = _view.ViewContext.RouteData.Values["hotelId"];
                if (routeHotelId != null)
                    return Convert.ToInt32(routeHotelId);
                var savedHotelId = _view.ViewData[ViewUtils.HotelIdKey];
                if (savedHotelId != null)
                    return (int) savedHotelId;
                throw new ApplicationException("No hotelId");
            }
        }

        public ControllerActionType ControllerActionType
        {
            get
            {
                var savedValue = _view.ViewData[ViewUtils.ControllerActionTypeKey];
                return savedValue != null ? (ControllerActionType)savedValue : ControllerActionType.Undefined; 
            }
        }

        public bool IsInvalidBoundedPeriod
        {
            get
            {
                var savedValue = _view.ViewData[ViewUtils.InvalidBoundedPeriodKey];
                return savedValue != null && (bool) savedValue;
            }
        }

        public bool IsApiServiceAvaliable
        {
            get
            {
                var savedValue = _view.ViewData[ViewUtils.ApiServiceNotResponse];
                return savedValue != null;
            }
        }

        public bool IsResourceNotFound
        {
            get
            {
                var savedValue = _view.ViewData[ViewUtils.ResourceNotFoundKey];
                return savedValue != null;
            }
        }

        public bool IsFreeBookingDatesConflict
        {
            get
            {
                var savedValue = _view.ViewData[ViewUtils.FreeBookingDatesConflictKey];
                return savedValue != null;
            }
        }
    }

    public class ViewDataProviderForController
    {
        private readonly Controller _controller;

        public ViewDataProviderForController(Controller controller)
        {
            _controller = controller;
        }

        public int HotelId
        {
            set { _controller.ViewData[ViewUtils.HotelIdKey] = value; }
        }

        public ControllerActionType ControllerActionType
        {
            set { _controller.ViewData[ViewUtils.ControllerActionTypeKey] = value; }
        }

        public void InvalidBoundedPeriod ()
        {
            _controller.ViewData[ViewUtils.InvalidBoundedPeriodKey] = true;
        }

        public void ApiServiceNotReponse()
        {
            _controller.ViewData[ViewUtils.ApiServiceNotResponse] = true;
        }

        public void ResourceNotFound()
        {
            _controller.ViewData[ViewUtils.ResourceNotFoundKey] = true;
        }

        public void FreeBookingDatesConflict()
        {
            _controller.ViewData[ViewUtils.FreeBookingDatesConflictKey] = true;
        }
    }
}