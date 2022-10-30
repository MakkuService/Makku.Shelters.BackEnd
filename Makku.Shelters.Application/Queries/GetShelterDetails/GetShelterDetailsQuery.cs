using MediatR;

namespace Makku.Shelters.Application.Queries.GetShelterDetails
{
    public class GetShelterDetailsQuery : IRequest<ShelterDetailsVm>

    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
