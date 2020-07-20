using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System;
using GigHub.Interfaces;

namespace GigHub.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig.ArtistId == userId)
            {
                if (gig.IsCanceled) return NotFound();

                gig.Cancel();

                _unitOfWork.Complete();

                return Ok();
            }
            else
                return BadRequest("Artist Unauthorized!");

        }
    }
}
