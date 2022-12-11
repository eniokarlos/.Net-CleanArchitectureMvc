using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository ?? 
                throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task AddAsync(CategoryDTO categoryDTO)
        {
            var CategoryEntity = mapper.Map<Category>(categoryDTO);
            await categoryRepository.CreateAsync(CategoryEntity);
        }

        public async Task RemoveAsync(int? id)
        {
            var CategoryEntity = categoryRepository.GetByIdAsync(id).Result;
            await categoryRepository.RemoveAsync(CategoryEntity);
        }

        public async Task<CategoryDTO> GetByIdAsync(int? id)
        {
            var CategoryEntity = await categoryRepository.GetByIdAsync(id);
            return mapper.Map<CategoryDTO>(CategoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var CategoriesEntity = await categoryRepository.GetCategoriesAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(CategoriesEntity);
        }

        public async Task UpdateAsync(CategoryDTO categoryDTO)
        {
            var CategoryEntity = mapper.Map<Category>(categoryDTO);
            await categoryRepository.UpdateAsync(CategoryEntity);
        }
    }
}