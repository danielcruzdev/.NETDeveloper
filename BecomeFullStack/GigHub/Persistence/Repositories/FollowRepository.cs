using GigHub.Interfaces;
using GigHub.Models;
using GigHub.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _context;
        #pragma warning disable IDE0052 // Remove unread private members
        private readonly GigRepository _gigRepository;
        #pragma warning restore IDE0052 // Remove unread private members


        public FollowRepository(ApplicationDbContext database)
        {
            _context = database;
            _gigRepository = new GigRepository(_context);

        }

        public IEnumerable<ApplicationUser> GetFollowingArtists(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }


        public bool IsFollowing(string userId, string ArtistId)
        {

            return _context.Followings
                    .Any(f => f.FolloweeId == ArtistId && f.FollowerId == userId);
        }

        public Following GetSingleFollower(string userId, string id)
        {
            return _context.Followings
                .SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == id);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following follower) 
        {
            _context.Followings.Remove(follower);
        }
    }
}