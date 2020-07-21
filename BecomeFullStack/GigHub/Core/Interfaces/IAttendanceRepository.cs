using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int id, string userId);
        bool IsAttending(string userId, int gigid);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}