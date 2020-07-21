using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Interfaces
{
    public interface IUserNotificationRepository
    {
        List<UserNotification> GetUserNotificationsFor(string userId);
    }
}