using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowRepository
    {
        private readonly ApplicationDbContext _dataBase;
        private readonly GigRepository _gigRepository;


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