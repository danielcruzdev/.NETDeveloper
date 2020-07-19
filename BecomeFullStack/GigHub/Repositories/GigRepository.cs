using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        private readonly ApplicationDbContext _dataBase;
        public GigRepository(ApplicationDbContext database)
        {
            _dataBase = database;
        }

        public IEnumerable<Gig> GetAllGigs()
        {
            return _dataBase.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _dataBase.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _dataBase.Attendances
                .Where(g => g.AttendeeId == userId)
                .Select(g => g.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetOpenGigsByArtistID(string artistId)
        {
            return _dataBase.Gigs
                 .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && g.IsCanceled == false)
                 .Include(g => g.Genre)
                 .ToList();
        }

        public Gig GetGigForEdit(string ArtistId, int gigId)
        {
            return _dataBase.Gigs.Single(g => g.Id == gigId && g.ArtistId == ArtistId);
        }

        public Gig GetGigDetails(int id)
        {
            return _dataBase.Gigs
                        .Include(g => g.Artist)
                        .Include(g => g.Genre)
                        .SingleOrDefault(g => g.Id == id);
        }
    }
}