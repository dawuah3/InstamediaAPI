using InstamediaApi.Models;
using System.Web.Http;

namespace InstamediaApi.Controllers.Api
{
    public class PictureController : ApiController
    {
        private ApplicationDbContext _context;

        public PictureController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Create(string userId, string description)
        {
            var pic = new Picture
            {
                Description = description
            };

            _context.Pictures.Add(pic);
            _context.SaveChanges();

            return Ok();
        }
    }
}
