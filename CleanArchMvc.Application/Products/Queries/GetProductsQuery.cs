using MediatR;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        
    }
}