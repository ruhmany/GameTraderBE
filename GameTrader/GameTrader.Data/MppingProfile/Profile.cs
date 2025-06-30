using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Data.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.MppingProfile
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<RefreshTokenDTO, RefreshToken>().ReverseMap();
            CreateMap<User, UserPageDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, UserDetailsDTO>()
             .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));


            CreateMap<AddUserDTO, User>()                
                  .ForMember(res => res.UserName, x => x.MapFrom(y => y.Email));
        }
    }
}
