using Makku.Shelters.Application.Interfaces;
using MediatR;

namespace Makku.Shelters.Application.Commands.DeleteShelter
{
    public class DeleteShelterCommandHandler : IRequestHandler<DeleteShelterCommand>
    {
        private readonly ISheltersDbContext _dbContext;
        public DeleteShelterCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteShelterCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Shelters
                .FindAsync(new object[] { request.Id }, cancellationToken);


            _dbContext.Shelters.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
