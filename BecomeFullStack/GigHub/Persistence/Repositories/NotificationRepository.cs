using GigHub.Interfaces;
using GigHub.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;
        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.UserID == userId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }

        List<UserNotification> INotificationRepository.MarkRead(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.UserID == userId && !u.IsRead)
                .ToList();
        }
    }
}