namespace GigHub.Interfaces
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendance { get; }
        IFollowRepository Follow { get; }
        IGenreRepository Genre { get; }
        IGigRepository Gigs { get; }
        INotificationRepository Notification { get; }
        IUserNotificationRepository UserNotifications { get; }
        IApplicationUserRepository Users { get; }

        void Complete();
    }
}