
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IcategoryService
    {
        Task<CategoryDTO> GetByIdAsync(int? id);
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task UpdateAsync(CategoryDTO categoryDTO);
        Task AddAsync(CategoryDTO categoryDTO);
        Task RemoveAsync(int? id);
    }
}