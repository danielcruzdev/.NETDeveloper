using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetArtistsFollowedBy(string userId);
    }
}