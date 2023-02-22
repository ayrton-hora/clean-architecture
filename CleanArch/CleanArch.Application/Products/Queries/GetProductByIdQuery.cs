using MediatR;

using CleanArch.Domain.Entities;

namespace CleanArch.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; private set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
