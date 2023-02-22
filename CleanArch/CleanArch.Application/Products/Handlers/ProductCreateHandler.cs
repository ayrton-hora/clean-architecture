using MediatR;

using CleanArch.Application.Products.Commands;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Products.Handlers
{
    public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            Product product = new(request.Name, request.Description, request.Price, request.Stock, request.Image);

            if (product is null) throw new ApplicationException("Error on creating entity");

            product.CategoryId = request.CategoryId;

            return await _productRepository.CreateAsync(product);
        }
    }
}
