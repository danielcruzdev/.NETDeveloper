using GigHub.Interfaces;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;


        public AttendanceRepository(ApplicationDbContext database)
        {
            _context = database;

        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                            .ToList();
        }

        public bool IsAttending(string userId, int gigid)
        {

            return _context.Attendances
                    .Any(a => a.GigId == gigid && a.AttendeeId == userId);
        }

        public Attendance GetAttendanceToDelete(int id, string userId)
        {
            return _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance) 
        {
            _context.Attendances.Remove(attendance);
        }
    }
}