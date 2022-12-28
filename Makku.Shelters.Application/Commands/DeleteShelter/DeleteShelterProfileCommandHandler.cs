using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using MediatR;

namespace Makku.Shelters.Application.Commands.DeleteShelter
{
    public class DeleteShelterProfileCommandHandler : IRequestHandler<DeleteShelterProfileCommand>
    {
        private readonly ISheltersDbContext _dbContext;
        public DeleteShelterProfileCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteShelterProfileCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Shelters
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Shelter), request.Id);
            }
            
            _dbContext.Shelters.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
