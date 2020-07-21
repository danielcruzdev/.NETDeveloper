using GigHub.Dto;
using GigHub.Interfaces;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _unitOfWork.Attendance.GetAttendance(dto.GigId, userId);
            if (exists != null)
                return BadRequest("The attendance already existis!");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendance.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendance.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendance.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }

    }
}
