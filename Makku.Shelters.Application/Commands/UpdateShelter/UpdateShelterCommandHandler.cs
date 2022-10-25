using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makku.Shelters.Application.Interfaces;
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

            entity.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
