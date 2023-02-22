using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetByIdAsync(int? id);

        // Task<Product> GetProductByCategoryAsync(int? categoryId);

        Task<Product> CreateAsync(Product product);
        
        Task<Product> UpdateAsync(Product product);
        
        Task DeleteAsync(Product product);
    }
}
