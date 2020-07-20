using AutoMapper;
using GigHub.App_Start;
using GigHub.Dtos;
using GigHub.Interfaces;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notification.GetNewNotifications(userId);


            return notifications.Select(Mapper.Map<Notification, NotificationDto>); ;
        }

        [HttpPost]
        public IHttpActionResult MarkRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notification.MarkRead(userId);

            notifications.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
