using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _dataBase;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;
        private readonly GenreRepository _genreRepository;
        private readonly FollowRepository _followRepository;

        public GigsController()
        {
            _dataBase = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_dataBase);
            _gigRepository = new GigRepository(_dataBase);
            _genreRepository = new GenreRepository(_dataBase);
            _followRepository = new FollowRepository(_dataBase);

        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _gigRepository.GetOpenGigsByArtistID(userId);

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }


        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigViewModel
            {
                Genres = _genreRepository.GetAllGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetAllGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _dataBase.Gigs.Add(gig);
            _dataBase.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        /* Update Method */

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _gigRepository.GetGigForEdit(id);

            if (gig == null) return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId()) return new HttpUnauthorizedResult();

            var genres = _genreRepository.GetAllGenres();

            var viewModel = new GigViewModel
            {
                Genres = genres,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig",
                Id = gig.Id
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetAllGenres();
                return View("GigForm", viewModel);
            }

            string userId = User.Identity.GetUserId();
            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);

            if (gig == null) return HttpNotFound();

            if (gig.ArtistId != userId) return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _dataBase.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var gig = _gigRepository.GetGigDetails(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel
            {
                Gig = gig
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _attendanceRepository.IsAttending(userId, gig.Id);

                viewModel.IsFollowing = _followRepository.IsFollowing(userId, gig.ArtistId);
            }

            return View("Details", viewModel);
        }
    }
}