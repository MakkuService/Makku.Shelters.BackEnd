using AutoMapper;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Domain;

namespace Makku.Shelters.Application.Queries.GetShelterList
{
    public class ShelterLookupDto : IMapWith<Shelter>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shelter, ShelterLookupDto>()
                .ForMember(shelterDto => shelterDto.Id, opt => opt.MapFrom(shelter => shelter.Id))
                .ForMember(shelterDto => shelterDto.Name, opt => opt.MapFrom(shelter => shelter.Name))
                .ForMember(shelterDto => shelterDto.UserId, opt => opt.MapFrom(shelter => shelter.UserId));
        }
    }
}
