using GigHub.Models;
using GigHub.ViewModels;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface IFollowRepository
    {
        void Add(Following following);
        void Remove(Following following);
        IEnumerable<ApplicationUser> GetFollowingArtists(string userId);

        bool IsFollowing(string userId, string ArtistId);
        Following GetSingleFollower(string userId, string id);
    }
}