using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Makku.Shelters.Application.Commands.UpdateShelter
{
    public class UpdateShelterCommand:IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

    }
}
