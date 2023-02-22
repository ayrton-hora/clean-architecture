using MediatR;

using CleanArch.Domain.Entities;

namespace CleanArch.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
