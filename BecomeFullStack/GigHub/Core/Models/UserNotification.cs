using GigHub.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
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

        protected UserNotification()
        {

        }

        [Key]
        [Column(Order = 1)]
        public string UserID { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }
    }
}