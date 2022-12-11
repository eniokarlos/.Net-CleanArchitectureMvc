using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using MediatR;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Application.Products.Commands;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public ProductService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator ?? 
                throw new ArgumentNullException(nameof(mediator));
        }

        public async Task AddAsync(ProductDTO productDTO)
        {
            var ProductCommand = mapper.Map<ProductCreateCommand>(productDTO);
            await mediator.Send(ProductCommand);
        }

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            var ProductQuery = new GetProductByIdQuery(id.Value);

            if(ProductQuery == null)
                throw new Exception($"Entity could not be loaded");

            var result = await mediator.Send(ProductQuery);
            return mapper.Map<ProductDTO>(result);

        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productsQuery = new GetProductsQuery();

            if(productsQuery == null)
                throw new Exception($"Entity could not be loaded");

            var result = await mediator.Send(productsQuery);
            return mapper.Map<IEnumerable<ProductDTO>>(result);

        }

        public async Task RemoveAsync(int? id)
        {
            var productCommand = new ProductRemoveCommand(id.Value);

            if(productCommand == null)
                throw new Exception($"Entity could not be loaded");

            await mediator.Send(productCommand);
        }

        public async Task UpdateAsync(ProductDTO productDTO)
        {
           var ProductCommand = mapper.Map<ProductUpdateCommand>(productDTO);
            await mediator.Send(ProductCommand);
        }
    }
}