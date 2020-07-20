using GigHub.Interfaces;
using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendance { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IFollowRepository Follow { get; private set; }
        public INotificationRepository Notification { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendance = new AttendanceRepository(context);
            Genre = new GenreRepository(context);
            Follow = new FollowRepository(context);
            Notification = new NotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}