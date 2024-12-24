using Application.MuscleGroups.Queries;
using Application.MuscleGroups.Queries.GetExercises;
using Application.MuscleGroups.Queries.GetMuscleGroup;
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
            return Ok(await Mediator.Send(new GetMuscleGroupsWithExercisesQuery()));
        }


        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<List<MuscleGroupDto>>> GetMuscleGroupsWithExercises()
        {
            return Ok(await Mediator.Send(new GetMuscleGroupsQuery()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MuscleGroupDto>> GetMuscleGroup(int id)
        {
            return Ok(await Mediator.Send(new GetMuscleGroupQuery { Id = id }));
        }

        [HttpGet("{bodyPartUrl}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ExerciseDto>>> GetExercisesForMuscleGroup(int id)
        {
            return Ok(await Mediator.Send(new GetExcercisesQuery { MuscleGroupId = id }));
        }
    }
}
