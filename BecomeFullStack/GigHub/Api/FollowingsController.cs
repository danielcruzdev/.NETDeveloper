﻿using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _context.Followings.Any(a => a.FollowerId == userId && a.FolloweeId == dto.FolloweeId);

            if (exists)
                return BadRequest("Following already existis!");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult unFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var follower = _context.Followings
                .SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == id);

            if (follower == null)
                return NotFound();

            _context.Followings.Remove(follower);
            _context.SaveChanges();

            return Ok(id);
        }

    }
}
