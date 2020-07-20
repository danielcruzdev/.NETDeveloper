using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        bool IsAttending(string userId, int gigid);
    }
}