using Application.MuscleGroups.Queries;
using Application.MuscleGroups.Queries.GetExercises;
using Application.MuscleGroups.Queries.GetMuscleGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalticsFitness.API.Controllers
{

    public class MuscleGroups : BaseController
    {
        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<List<MuscleGroupDto>>> GetMuscleGroups()
        {
            return Ok(await Mediator.Send(new GetMuscleGroupsQuery()));
        }


        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<List<MuscleGroupDto>>> GetMuscleGroupsWithExercises()
        {
            return Ok(await Mediator.Send(new GetMuscleGroupsWithExercisesQuery()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MuscleGroupDto>> GetMuscleGroup(int id)
        {
            return Ok(await Mediator.Send(new GetExcercisesForMuscleGroupQuery(id)));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetExercisesResult>> GetExercisesForMuscleGroup(int id)
        {
            return Ok(await Mediator.Send(new GetExcercisesForMuscleGroupQuery(id)));
        }
    }
}
