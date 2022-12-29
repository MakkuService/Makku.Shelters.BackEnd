using MediatR;

namespace Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterList
{
    public class GetShelterListQuery : IRequest<ShelterListVm>
    {
        public Guid UserId { get; set; }
    }
}
