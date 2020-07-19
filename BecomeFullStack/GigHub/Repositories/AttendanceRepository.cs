using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _dataBase;
        private readonly GigRepository _gigRepository;


        public AttendanceRepository(ApplicationDbContext database)
        {
            _dataBase = database;
            _gigRepository = new GigRepository(_dataBase);

        }


        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _dataBase.Attendances
                            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                            .ToList();
        }

        public bool IsAttending(string userId, int gigid)
        {

            return _dataBase.Attendances
                    .Any(a => a.GigId == gigid && a.AttendeeId == userId);
        }
    }
}