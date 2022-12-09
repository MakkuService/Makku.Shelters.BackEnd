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
        public string? Address { get; set; }
        public string? Coordinate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Description { get; set; }
        public decimal? CumulativeDonate { get; set; }
        public decimal? Donation { get; set; }
        public string? Problems { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public string? Inn { get; set; }
        public string? Ogrn { get; set; }
        public string? BankAccount { get; set; }
        public string? Ceo { get; set; }
        public decimal? SummDonation { get; set; }
        public DateTime? FoundDate { get; set; }
        public int? NumberOfPeople { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shelter, ShelterLookupDto>()
                .ForMember(shelterDto => shelterDto.Id, opt => opt.MapFrom(shelter => shelter.Id))
                .ForMember(shelterDto => shelterDto.Name, opt => opt.MapFrom(shelter => shelter.Name))
                .ForMember(shelterDto => shelterDto.UserId, opt => opt.MapFrom(shelter => shelter.UserId));
        }
    }
}
