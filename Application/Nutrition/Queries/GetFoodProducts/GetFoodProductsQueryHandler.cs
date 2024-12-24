using Application.Data;
using Application.Support.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Nutrition.Queries.GetFoodProducts
{
    public class GetFoodProductsQueryHandler : IRequestHandler<GetFoodProductsQuery, List<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;

        public GetFoodProductsQueryHandler(IMapper mapper, ITrainingDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductDto>> Handle(GetFoodProductsQuery request, CancellationToken cancellationToken)
        {
            var foodType = await _context.FoodTypes
               .FirstOrDefaultAsync(c => c.Name.Equals(request.FoodTypeUrl, StringComparison.OrdinalIgnoreCase),cancellationToken);


            if (foodType == null)
            {
                throw new FoodTypeNotFoundException(nameof(FoodTypeDto), request.FoodTypeUrl);
            }


            var products = foodType.Products;
            if (products.Count.Equals(0) || products == null)
            {
                throw new ProductsNotFoundException(nameof(products), request.FoodTypeUrl);
            }

            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return productDtos;
        }
    }
}
