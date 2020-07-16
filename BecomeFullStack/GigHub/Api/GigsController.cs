using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System;

namespace GigHub.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _database;
        public GigsController()
        {
            _database = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _database.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled) return NotFound();

            gig.Cancel();

            _database.SaveChanges();

            return Ok();
        }
    }
}
