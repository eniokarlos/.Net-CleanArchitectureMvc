

using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> GetByIdAsync(int? id);
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task UpdateAsync(ProductDTO productDTO);
        Task AddAsync(ProductDTO productDTO);
        Task RemoveAsync(int? id);
        Task<ProductDTO> GetProductCategoryAsync(int? id);

    }
}