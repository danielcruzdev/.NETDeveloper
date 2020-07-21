using System;

namespace GigHub.Models
{
    public class UserNotification
    {

        public string UserID { get; private set; }

        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }


        protected UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            User = user ?? throw new ArgumentNullException("Null user!");
            Notification = notification ?? throw new ArgumentNullException("Null notification!");

            User = user;
            UserID = user.Id;
            Notification = notification;
            NotificationId = notification.Id;
        }

        public void Read()
        {
            IsRead = true;
        }

    }
}