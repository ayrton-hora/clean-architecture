using CleanArch.Application.DTOs;

namespace CleanArch.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();

        Task<CategoryDTO> GetByIdAsync(int? id);

        Task<CategoryDTO> AddAsync(CategoryDTO categoryDTO);

        Task UpdateAsync(CategoryDTO categoryDTO);

        Task RemoveAsync(int? id);
    }
}
