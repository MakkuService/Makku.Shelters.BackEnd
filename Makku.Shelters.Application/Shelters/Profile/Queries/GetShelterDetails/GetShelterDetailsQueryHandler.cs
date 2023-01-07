using AutoMapper;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterDetails
{
    public class GetShelterDetailsQueryHandler : IRequestHandler<GetShelterDetailsQuery, ShelterDetailsVm>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetShelterDetailsQueryHandler(ISheltersDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ShelterDetailsVm> Handle(GetShelterDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.ShelterProfiles.FirstOrDefaultAsync(shelter => shelter.ShelterProfileId == request.Id, cancellationToken);

            if (entity == null || entity.ShelterProfileId != request.Id)
            {
                //todo сделать кастомные эксепшны и обновить в аналогичных местах
                throw new Exception(nameof(ShelterProfile));
            }

            return _mapper.Map<ShelterDetailsVm>(entity);
        }
    }
}