using AutoMapper;
using WorkoutGlobal.Api.Models.DTOs.UserDTOs;

namespace WorkoutGlobal.Api.Models.Mapping
{
    /// <summary>
    /// Class for configure mapping rules via AutoMapper library.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Sets mapping rules for models and their DTOs.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserAuthorizationDto, User>();
        }
    }
}
