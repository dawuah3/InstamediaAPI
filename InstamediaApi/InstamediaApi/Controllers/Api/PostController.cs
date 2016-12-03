using InstamediaApi.Filters;
using InstamediaApi.Models;
using InstamediaApi.Services;
using System.Linq;
using System.Web.Http;

namespace InstamediaApi.Controllers.Api
{
    [InstamediaAuthorize]
    public class PostController : ApiController
    {
        private ApplicationDbContext _context;
        private IDJsIdentityService _identityService;


        public PostController(IDJsIdentityService identityService)
        {
            _context = new ApplicationDbContext();
            _identityService = identityService;
        }

        [HttpPost]
        public IHttpActionResult Create(string description)
        {
            var user = _identityService.CurrentUser;

            var userId = _context.Users.Single(u => u.Username == user).Id.ToString();
            var post = new Post
            {
                UserId = userId,
                Description = description
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return Ok();
        }

    }
}
