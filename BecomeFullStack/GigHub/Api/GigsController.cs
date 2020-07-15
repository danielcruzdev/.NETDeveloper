using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _database;
        public GigsController()
        {
            _database = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _database.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            gig.IsCanceled = true;
            _database.SaveChanges();

            return Ok();
        }
    }
}
