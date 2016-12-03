using InstamediaApi.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace InstamediaApi.Filters
{
    public class InstamediaAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var rawCredentials = authHeader.Parameter;
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                    var split = credentials.Split(':');
                    var username = split[0];
                    var password = split[1];

                    //if (!WebSecurity.Initialized)
                    //{
                    //    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id",
                    //        "Username", autoCreateTables: true);
                    //}

                    // Can replace this with custom login
                    //if (WebSecurity.Login(username, password))
                    if (UserLogin(username, password))
                    {
                        var principal = new GenericPrincipal(new GenericIdentity(username), null);
                        Thread.CurrentPrincipal = principal;

                        return;
                    }
                }
            }

            HandleUnauthorized(actionContext);
        }

        private bool UserLogin(string username, string password)
        {
            var _context = new ApplicationDbContext();
            if (_context.Users.Any(u => u.Username == username && u.Password == password))
            {
                return true;
                //return _context.Users.Where(u => u.Username == username).Select(Mapper.Map<User, UserDto>);
            }
            return false;
        }

        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Instamedia' location='http://localhost:17435/account/login");
        }
    }
}