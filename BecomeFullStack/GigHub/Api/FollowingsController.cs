using GigHub.Dto;
using GigHub.Interfaces;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _unitOfWork.Follow.IsFollowing(userId, dto.FolloweeId);
                
            if (exists)
                return BadRequest("Following already existis!");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _unitOfWork.Follow.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult unFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var follower = _unitOfWork.Follow.GetSingleFollower(userId, id);

            if (follower == null)
                return NotFound();


            _unitOfWork.Follow.Remove(follower);
            _unitOfWork.Complete();

            return Ok(id);
        }

    }
}
