using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotifications(string userId);
        List<UserNotification> MarkRead(string userId);
    }
}