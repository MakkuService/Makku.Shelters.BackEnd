using AutoMapper;
using Makku.Shelters.Domain;

namespace Makku.Shelters.Application.Queries.GetShelterList
{
    public class ShelterLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shelter, ShelterLookupDto>()
                .ForMember(shelterDto => shelterDto.Id, opt => opt.MapFrom(note => note.Id))
                .ForMember(shelterDto => shelterDto.Name, opt => opt.MapFrom(note => note.Name));
        }
    }
}
