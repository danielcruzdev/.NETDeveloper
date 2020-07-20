using GigHub.Interfaces;
using GigHub.Models;
using GigHub.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _dataBase;
        #pragma warning disable IDE0052 // Remove unread private members
        private readonly GigRepository _gigRepository;
        #pragma warning restore IDE0052 // Remove unread private members


        public FollowRepository(ApplicationDbContext database)
        {
            _dataBase = database;
            _gigRepository = new GigRepository(_dataBase);

        }

        public IEnumerable<ApplicationUser> GetFollowingArtists(string userId)
        {
            return _dataBase.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }


        public bool IsFollowing(string userId, string ArtistId)
        {

            return _dataBase.Followings
                    .Any(f => f.FolloweeId == ArtistId && f.FollowerId == userId);
        }
    }
}