using System;
using System.Collections;
using System.Linq;
using System.Web.Http;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace RestTraining.Api.Controllers
{
    public class AccountController : ApiController
    {
        public bool Post(LogOnModel model)
        {
            if (model.Username == "1" && model.Password == "1")
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return true;
            }
            return false;
        }
    }
}
