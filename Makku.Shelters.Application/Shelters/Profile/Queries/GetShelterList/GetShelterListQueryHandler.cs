using AutoMapper;
using AutoMapper.QueryableExtensions;
using Makku.Shelters.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterList
{
    public class GetShelterListQueryHandler : IRequestHandler<GetShelterListQuery, ShelterListVm>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetShelterListQueryHandler(ISheltersDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ShelterListVm> Handle(GetShelterListQuery request, CancellationToken cancellationToken)
        {
            var shelterQuery = await _dbContext.Shelters
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<ShelterLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ShelterListVm { Shelters = shelterQuery };
        }
    }
}
