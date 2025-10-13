using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands
{
    public class DeleteProductByIdCommand : IRequest<bool>
    {
        public string Id { get; init; }

        public DeleteProductByIdCommand(string id)
        {
            Id = id;
        }
    }
}
