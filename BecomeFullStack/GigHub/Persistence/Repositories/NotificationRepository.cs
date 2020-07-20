using GigHub.Dtos;
using GigHub.Interfaces;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext database)
        {
            _context = database;
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