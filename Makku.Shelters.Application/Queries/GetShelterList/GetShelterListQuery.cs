using MediatR;

namespace Makku.Shelters.Application.Queries.GetShelterList
{
    public class GetShelterListQuery : IRequest<ShelterListVm>
    {
        public Guid UserId { get; set; }
    }
}
