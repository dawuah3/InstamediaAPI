using AutoMapper;
using InstamediaApi.Dtos;
using InstamediaApi.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace InstamediaApi.Controllers.Api
{
    //[IdentityBasicAuthentication]
    //[Authorize]
    public class UserController : ApiController
    {
        private ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        // POST : /api/users/register/username/password
        [HttpPost]
        public IHttpActionResult Register(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
                return BadRequest();

            var user = new User
            {
                Username = username,
                Password = password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        // POST : /api/users/login/username/password
        [HttpPost]
        public IEnumerable<UserDto> Login(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username && u.Password == password))
            {
                return  _context.Users.Where(u => u.Username == username).Select(Mapper.Map<User, UserDto>);
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content =
                        new StringContent($"No user with Username = {username} or password {password}"),
                    ReasonPhrase = "This username/password combination does not exist"
                };
                throw new HttpResponseException(resp);
            }
        }

        [HttpPost]
        public string Info(string id)
        {
            var nameValueCollection = ConfigurationManager.AppSettings.AllKeys.ToString();
            nameValueCollection = Assembly.GetExecutingAssembly().FullName;
            return nameValueCollection;
        }
    }

    //public class IdentityBasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    //{
    //    public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
    //    {
    //        // 1. Look for credentials in the request.
    //        HttpRequestMessage request = context.Request;
    //        AuthenticationHeaderValue authorization = request.Headers.Authorization;

    //        // 2. If there are no credentials, do nothing.
    //        if (authorization == null)
    //        {
    //            return;
    //        }

    //        // 3. If there are credentials but the filter does not recognize the 
    //        //    authentication scheme, do nothing.
    //        if (authorization.Scheme != "Basic")
    //        {
    //            return;
    //        }

    //        // 4. If there are credentials that the filter understands, try to validate them.
    //        // 5. If the credentials are bad, set the error result.
    //        if (String.IsNullOrEmpty(authorization.Parameter))
    //        {
    //            context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
    //            return;
    //        }

    //        Tuple<string, string> userNameAndPasword = ExtractUserNameAndPassword(authorization.Parameter);
    //        if (userNameAndPasword == null)
    //        {
    //            context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
    //        }

    //        string userName = userNameAndPasword.Item1;
    //        string password = userNameAndPasword.Item2;

    //        IPrincipal principal = await AuthenticateAsync(userName, password, cancellationToken);
    //        if (principal == null)
    //        {
    //            context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
    //        }

    //        // 6. If the credentials are valid, set principal.
    //        else
    //        {
    //            context.Principal = principal;
    //        }

    //    }
    //}

    //public class AuthenticationFailureResult : IHttpActionResult
    //{
    //    public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
    //    {
    //        ReasonPhrase = reasonPhrase;
    //        Request = request;
    //    }

    //    public string ReasonPhrase { get; private set; }

    //    public HttpRequestMessage Request { get; private set; }

    //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(Execute());
    //    }

    //    private HttpResponseMessage Execute()
    //    {
    //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
    //        response.RequestMessage = Request;
    //        response.ReasonPhrase = ReasonPhrase;
    //        return response;
    //    }
    //}

    //public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    //{
    //    public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
    //    {
    //        Challenge = challenge;
    //        InnerResult = innerResult;
    //    }

    //    public AuthenticationHeaderValue Challenge { get; private set; }

    //    public IHttpActionResult InnerResult { get; private set; }

    //    public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);

    //        if (response.StatusCode == HttpStatusCode.Unauthorized)
    //        {
    //            // Only add one challenge per authentication scheme.
    //            //if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
    //            if (response.Headers.WwwAuthenticate.All(h => h.Scheme != Challenge.Scheme))
    //            {
    //                response.Headers.WwwAuthenticate.Add(Challenge);
    //            }
    //        }

    //        return response;
    //    }
    //}
}
