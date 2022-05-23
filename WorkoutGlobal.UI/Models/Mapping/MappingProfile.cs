﻿using AutoMapper;
using WorkoutGlobal.UI.ViewModels;
using WorkoutGlobal.UI.ViewModels.Authentication;

namespace WorkoutGlobal.UI.Models.Mapping
{
    /// <summary>
    /// Represents auto mapping rules.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Sets mapping rules.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<UserAuthorizationViewModel, AuthenticationUser>();
            CreateMap<UserRegistrationViewModel, UserCredentials>();

            CreateMap<RegistrationUser, UserRegistrationViewModel>().ReverseMap();

            CreateMap<Video, VideoViewModel>().ReverseMap();
        }
    }
}
