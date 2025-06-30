using AutoMapper;
using GameTrader.Core.DTOs.UserDTOs;

namespace GameTrader.API.Models
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginModel, LoginDTO>().ReverseMap();
            CreateMap<AddUserModel, AddUserDTO>();
            CreateMap<EditUserModel, EditUserDTO>();
        }
    }
}
