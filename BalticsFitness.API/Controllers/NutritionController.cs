//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Application.Nutrition;
//using Application.Nutrition.Queries.GetFoodTypes;
//using Application.Nutrition.Queries.GetFoodType;
//using Application.Nutrition.Queries.GetFoodProducts;

//namespace BalticsFitness.API.Controllers
//{
//    public class NutritionController : BaseController
//    {
//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<ActionResult<List<FoodTypeDto>>> GetFoodTypes()
//        {
//            return Ok(await Mediator.Send(new GetFoodTypesQuery()));
//        }

//        [HttpGet("{id}")]
//        [AllowAnonymous]
//        public async Task<ActionResult<FoodTypeDto>> GetFoodType(int id)
//        {
//            return Ok(await Mediator.Send(new GetFoodTypeQuery(id)));
//        }

//        [HttpGet("{foodTypeUrl}")]
//        [AllowAnonymous]
//        public async Task<ActionResult<List<ProductDto>>> GetProductsByFoodType(string foodTypeUrl)
//        {
//            return Ok(await Mediator.Send(new GetFoodProductsQuery(foodTypeUrl)));
//        }
//    }
//}
