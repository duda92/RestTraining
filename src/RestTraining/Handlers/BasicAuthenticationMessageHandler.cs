using System;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using RestTraining.Common.Authorization;

namespace RestTraining.Api.Handlers
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                return base.SendAsync(request, cancellationToken);
            }

            var authHeader = request.Headers.Authorization;

            if (authHeader == null)
            {
                return base.SendAsync(request, cancellationToken);
            }

            if (authHeader.Scheme != "Basic")
            {
                return base.SendAsync(request, cancellationToken);
            }

            var encodedUserPass = authHeader.Parameter.Trim();
            var userPass = Encoding.ASCII.GetString(Convert.FromBase64String(encodedUserPass));
            var parts = userPass.Split(":".ToCharArray());
            var username = parts[0];
            var password = parts[1];
            if (!request.Headers.Any(x => x.Key == "appId"))
                return base.SendAsync(request, cancellationToken);

            var appId = request.Headers.FirstOrDefault(x => x.Key == "appId").Value.FirstOrDefault();
            if (appId == null)
                return base.SendAsync(request, cancellationToken); 
            
            var privateKeyForApp = GetPrivateKeyForApp(appId);
            if (privateKeyForApp == null)
                return base.SendAsync(request, cancellationToken); 

            var decriptor = new RSA();
            var decriptedPassword = decriptor.DecryptString(password, privateKeyForApp);

            if (!Membership.ValidateUser(username, decriptedPassword))
            {
                return base.SendAsync(request, cancellationToken);
            }

            var identity = new GenericIdentity(username, "Basic");
            var principal = new GenericPrincipal(identity, new string[] { });
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return base.SendAsync(request, cancellationToken);
        }

        private string GetPrivateKeyForApp(string appId)
        {
            var pair = WebApiApplication.privateKeys.Where(x => x.Key == appId).Select(p => new { Key = p.Key, Value = p.Value }).FirstOrDefault();
            return pair != null ? pair.Value : null;
        }
    }
}