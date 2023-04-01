using CleanArch.Application.DTOs;

namespace CleanArch.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();

        Task<ProductDTO> GetByIdAsync(int? id);

        // Task<ProductDTO> GetProductByCategoryIdAsync(int? id);

        Task<ProductDTO> AddAsync(ProductDTO productDTO);
        
        Task UpdateAsync(ProductDTO productDTO);

        Task DeleteAsync(int? id);
    }
}
