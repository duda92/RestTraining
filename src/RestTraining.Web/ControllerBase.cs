using System.Web.Mvc;
using RestTraining.Common.Proxy;

namespace RestTraining.Web
{
    public abstract class ControllerBase : Controller
    {
        public const string BaseUrl = "http://localhost.:9075";

        protected JsonRequestExecutor executor = new JsonRequestExecutor("client1", "client1", MvcApplication.publicKey, MvcApplication.appId);//this.User.Identity.Name
        
        protected ViewDataProviderForController _viewDataProvider;
        
        protected ControllerBase()
        {
            _viewDataProvider = new ViewDataProviderForController(this);
        }
    }
}