using AutoMapper;
using Makku.Shelters.Application.Commands.CreateShelter;
using Makku.Shelters.Application.Common.Mappings;

namespace Makku.Shelters.WebApi.Models
{
    public class CreateShelterDto : IMapWith<CreateShelterCommand>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateShelterDto, CreateShelterCommand>()
                .ForMember(shelterCommand => shelterCommand.Name,
                    opt => opt.MapFrom(shelterDto => shelterDto.Name));
        }
    }
}
