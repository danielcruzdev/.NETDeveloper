using GigHub.Interfaces;
using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {

        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        List<UserNotification> IUserNotificationRepository.GetUserNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserID == userId && !un.IsRead)
                .ToList();
        }
    }
}