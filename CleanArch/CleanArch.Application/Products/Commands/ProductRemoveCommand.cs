using MediatR;

namespace CleanArch.Application.Products.Commands
{
    public class ProductRemoveCommand : IRequest
    {
        public int Id { get; private set; }

        public ProductRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
