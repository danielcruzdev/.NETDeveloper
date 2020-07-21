using GigHub.Interfaces;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;
        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetAllGigs()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(g => g.AttendeeId == userId && g.Gig.DateTime > DateTime.Now)
                .Select(g => g.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetOpenGigsByArtistID(string artistId)
        {
            return _context.Gigs
                 .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCanceled)
                 .Include(g => g.Genre)
                 .ToList();
        }

        public Gig GetGigForEdit(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigDetails(int id)
        {
            return _context.Gigs
                        .Include(g => g.Artist)
                        .Include(g => g.Genre)
                        .SingleOrDefault(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}