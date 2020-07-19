using GigHub.Models;
using GigHub.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FollowRepository _followRepository;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
            _followRepository = new FollowRepository(_context);
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var artists = _followRepository.GetFollowingArtists(userId);

            return View(artists);
        }
    }
}