using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using MediatR;

namespace Makku.Shelters.Application.Commands.CreateShelter
{
    public class CreateShelterCommandHandler : IRequestHandler<CreateShelterCommand, Guid>
    {
        private readonly ISheltersDbContext _dbContext;
        public CreateShelterCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;
        public async Task<Guid> Handle(CreateShelterCommand request, CancellationToken cancellationToken)
        {
            var shelter = new Shelter()
            {
                Name = request.Name
            };

            await _dbContext.Shelters.AddAsync(shelter, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return shelter.Id;
        }
    }
}
