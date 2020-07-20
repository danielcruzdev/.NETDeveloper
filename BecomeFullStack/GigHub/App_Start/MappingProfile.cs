using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.ViewModels;

namespace GigHub.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();

        }
    }
}