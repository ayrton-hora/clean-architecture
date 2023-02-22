using Microsoft.EntityFrameworkCore;

using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;

namespace CleanArch.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;

        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _productContext.Add(product);

            await _productContext.SaveChangesAsync();

            return product;
        }

        public async Task DeleteAsync(Product product)
        {
            _productContext.Remove(product);

            await _productContext.SaveChangesAsync();
        }

        //public async Task<Product> GetProductByCategoryAsync(int? categoryId)
        //{
        //    return await _productContext.Products.Include(p => p.Category)
        //        .SingleOrDefaultAsync(p => p.Id == categoryId);
        //}

        public async Task<Product> GetByIdAsync(int? id)
        {
            // return await _productContext.Products.FindAsync(id);
            return await _productContext.Products.Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productContext.Update(product);

            await _productContext.SaveChangesAsync();

            return product;
        }
    }
}
