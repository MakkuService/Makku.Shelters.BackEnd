using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Application.Identity.Commands.LoginShelter;

namespace Makku.Shelters.WebApi.Models
{
    public class LoginShelterDto : IMapWith<LoginShelterCommand>
    {
        [EmailAddress]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginShelterDto, LoginShelterCommand>()
                .ForMember(dest => dest.Username, opt
                    => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt
                    => opt.MapFrom(src => src.Password));
        }

    }
}
