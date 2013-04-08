﻿using System.Web.Mvc;
using RestTraining.Common.Proxy;

namespace RestTraining.Web
{
    public abstract class ControllerBase : Controller
    {
        public const string BaseUrl = "http://localhost.:9075";

        public const JsonRequestExecutor Instructio = "http://localhost.:9075";
        
        protected ViewDataProviderForController _viewDataProvider;
        


        protected ControllerBase()
        {
            _viewDataProvider = new ViewDataProviderForController(this);
        }
    }
}