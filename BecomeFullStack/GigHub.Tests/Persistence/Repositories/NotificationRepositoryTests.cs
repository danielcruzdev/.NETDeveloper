using FluentAssertions;
using GigHub.Interfaces;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.Tests.Extensions;
using GigHub.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object);


            _repository = new NotificationRepository(mockContext.Object);
        }


        [TestMethod]
        public void GetNewNotifications_NotificationIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotifications(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotifications_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotifications(user.Id + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotifications_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotifications(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);
        }
    }
}
