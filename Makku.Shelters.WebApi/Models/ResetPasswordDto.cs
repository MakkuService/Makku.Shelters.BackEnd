using AutoMapper;
using Destructurama.Attributed;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Application.Shelters.Identity.Commands.LoginShelter;
using System.ComponentModel.DataAnnotations;
using Makku.Shelters.Application.Shelters.Identity.Commands.ResetPassword;

namespace Makku.Shelters.WebApi.Models
{
    public class ResetPasswordDto : IMapWith<LoginShelterCommand>
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [NotLogged]
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResetPasswordDto, ResetPasswordCommand>()
                .ForMember(dest => dest.Email, opt
                    => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt
                    => opt.MapFrom(src => src.Password));
        }
    }
}
