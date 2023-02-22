using MediatR;

using CleanArch.Application.Products.Commands;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Products.Handlers
{
    public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductUpdateHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null) throw new ApplicationException("Entity could not be found");

            product.Update(request.Name, request.Description, request.Price, request.Stock, request.Image, request.CategoryId);

            return await _productRepository.UpdateAsync(product);
        }
    }
}
