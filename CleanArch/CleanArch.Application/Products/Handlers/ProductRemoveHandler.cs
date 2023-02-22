using MediatR;

using CleanArch.Application.Products.Commands;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Products.Handlers
{
    public class ProductRemoveHandler : IRequestHandler<ProductRemoveCommand>
    {
        private readonly IProductRepository _productRepository;

        public ProductRemoveHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null) throw new ApplicationException("Entity could not be found");

            await _productRepository.DeleteAsync(product);
        }
    }
}
