using InstamediaApi.Filters;
using InstamediaApi.Models;
using InstamediaApi.Services;
using System.Linq;
using System.Web.Http;

namespace InstamediaApi.Controllers.Api
{
    [InstamediaAuthorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        private IDJsIdentityService _identityService;

        public FollowingsController(IDJsIdentityService identityService)
        {
            _context = new ApplicationDbContext();
            _identityService = identityService;
        }


        // POST: /api/followers/follow?followerId=1&id=2
        [HttpPost]
        public IHttpActionResult Follow(string followerId, string followeeId)
        {
            if (_context.Followings.Any(f => f.FolloweeId == followeeId && f.FollowerId == followerId))
                return BadRequest("Following already exists");

            var following = new Following
            {
                FolloweeId = followeeId,
                FollowerId = followerId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Test(string id)
        {
            var user = _identityService.CurrentUser.ToString();
            string userId;
            var user2 = _context.Users.Where(u => u.Username == user).Select(u => new
            {
                UserId = u.Id
            }).Single();

            if (_context.Followings.Any(f => f.FolloweeId == id && f.FollowerId == user))
                return BadRequest("Following already exists");
            
            var following = new Following
            {
                FolloweeId = id,
                FollowerId = user2.UserId.ToString()
            };

            _context.Followings.Add(following);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}
