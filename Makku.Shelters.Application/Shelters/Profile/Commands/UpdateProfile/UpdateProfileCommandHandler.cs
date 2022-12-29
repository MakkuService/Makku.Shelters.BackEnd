using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler: IRequestHandler<UpdateProfileCommand>
    {
        private readonly ISheltersDbContext _dbContext;
        public UpdateProfileCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateProfileCommand request,
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
