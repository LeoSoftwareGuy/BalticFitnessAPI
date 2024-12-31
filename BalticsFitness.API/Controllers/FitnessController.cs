using Application.Trainings.Commands.SaveMealCommand;
using Application.Trainings.Commands.SaveTrainingCommand;
using Application.Trainings.DTOs.Nutrition;
using Application.Trainings.DTOs.Trainings;
using Application.Trainings.Queries.GetTrainings;
using Microsoft.AspNetCore.Mvc;

namespace BalticsFitness.API.Controllers
{
    public class FitnessController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<SortedByDayTraining>>> GetTrainings()
        {
            return Ok(await Mediator.Send(new GetTrainingsSortedByDay()));
        }

        //[HttpGet("allMeals")]
        //public async Task<ActionResult<List<SortedByDayNutrients>>> GetMeals( )
        //{
        //    return Ok(await Mediator.Send(new GetMealsRequest()));
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]    
        public async Task<IActionResult> SaveTraining(SaveTrainingCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> SaveMeal(SaveMealCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

    }
}
