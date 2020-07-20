using GigHub.Interfaces;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var artists = _unitOfWork.Follow.GetFollowingArtists(userId);

            return View(artists);
        }
    }
}