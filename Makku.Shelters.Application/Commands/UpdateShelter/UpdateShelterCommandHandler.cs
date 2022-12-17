using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Commands.UpdateShelter
{
    public class UpdateShelterCommandHandler: IRequestHandler<UpdateShelterCommand>
    {
        private readonly ISheltersDbContext _dbContext;
        public UpdateShelterCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateShelterCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Shelters.FirstOrDefaultAsync(note =>
                    note.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Shelter), request.Id);
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
