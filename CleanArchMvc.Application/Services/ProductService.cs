using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository ?? 
                throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task AddAsync(ProductDTO productDTO)
        {
            var ProductEntity = mapper.Map<Product>(productDTO);
            await productRepository.CreateAsync(ProductEntity);
        }

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            var productEntity = await productRepository.GetByIdAsync(id);
            return mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<ProductDTO> GetProductCategoryAsync(int? id)
        {
            var productEntity = await productRepository.GetProductCategoryAsync(id);
            return mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var products = await productRepository.GetProductsAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task RemoveAsync(int? id)
        {
            var productEntity = productRepository.GetByIdAsync(id).Result;
            await productRepository.RemoveAsync(productEntity);
        }

        public async Task UpdateAsync(ProductDTO productDTO)
        {
            var productEntity = mapper.Map<Product>(productDTO);
            await productRepository.UpdateAsync(productEntity);
        }
    }
}