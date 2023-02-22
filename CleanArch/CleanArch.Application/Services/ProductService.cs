using AutoMapper;
using MediatR;

using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Products.Commands;
using CleanArch.Application.Products.Queries;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task AddAsync(ProductDTO productDTO)
        {
            ProductCreateCommand command = new();

            var mappedEntity = _mapper.Map<ProductCreateCommand>(productDTO);

            await _mediator.Send(mappedEntity);
        }

        public async Task DeleteAsync(int? id)
        {
            ProductRemoveCommand command = new(id.Value);

            if (command is null) throw new Exception("Entity could not be loaded");

            await _mediator.Send(command);
        }

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            GetProductByIdQuery query = new(id.Value);

            if (query is null) throw new Exception("Entity could not be loaded");

            Product response = await _mediator.Send(query);

            return _mapper.Map<ProductDTO>(response);
        }

        //public async Task<ProductDTO> GetProductByCategoryIdAsync(int? id)
        //{
        //    GetProductByIdQuery query = new(id.Value);

        //    if (query is null) throw new Exception("Entity could not be loaded");

        //    Product response = await _mediator.Send(query);

        //    return _mapper.Map<ProductDTO>(response);
        //}

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            GetProductsQuery query = new();

            if (query is null) throw new Exception("Entity could not be loaded");

            IEnumerable<Product> response = await _mediator.Send(query);

            return _mapper.Map<IEnumerable<ProductDTO>>(response);
        }

        public async Task UpdateAsync(ProductDTO productDTO)
        {
            ProductUpdateCommand command = new();

            var mappedEntity = _mapper.Map<ProductUpdateCommand>(productDTO);

            await _mediator.Send(mappedEntity);
        }
    }
}
