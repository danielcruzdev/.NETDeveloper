using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        IEnumerable<Gig> GetAllGigs();
        Gig GetGigDetails(int id);
        Gig GetGigForEdit(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetOpenGigsByArtistID(string artistId);
    }
}