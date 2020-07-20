using GigHub.Models;
using GigHub.ViewModels;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface IFollowRepository
    {
        IEnumerable<ApplicationUser> GetFollowingArtists(string userId);
        bool IsFollowing(string userId, string ArtistId);
    }
}