using Microsoft.AspNetCore.Mvc;
using Application.MonthlyStatistics;
using Application.MonthlyStatistics.Queries.GetMonthlyStats;
using Application.MonthlyStatistics.Queries.GetSummaryInfoBasedOnFilter;
using Application.MonthlyStatistics.Queries.GetBestExerciseStats;
using Application.MonthlyStatistics.Queries.GetLatestExerciseStats;
using Application.MonthlyStatistics.Queries.GetExercisesSetsForAnExerciseLastThreeMonths;

namespace BalticsFitness.API.Controllers
{
    public class StatisticsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<StatResults>> GetMonthlyStats()
        {
            return Ok(await Mediator.Send(new GetMonthlyStatsQuery()));
        }

        [HttpGet("filterBy")]
        public async Task<ActionResult<SummaryInfoBasedOnFilter>> GetStats(string filterBy)
        {
            return Ok(await Mediator.Send(new GetSummaryInfoBasedOnFilterQuery(filterBy)));
        }

        [HttpGet("{exerciseId}/{type}")]
        public async Task<ActionResult<ExerciseStats>> GetBestExerciseStats(int exerciseId, string type)
        {
            if (type == "Best")
            {
                // Handle "Best" type
                return Ok(await Mediator.Send(new GetBestExerciseStatsQuery(exerciseId)));
            }
            else
            {
                // Handle "Last" type
                return Ok(await Mediator.Send(new GetLatestExerciseStatsQuery(exerciseId)));
            }
        }

        [HttpGet("{exerciseId}")]
        public async Task<ActionResult<ExerciseStatsLastThreeMonths>> GetExerciseHistory(int exerciseId)
        {
            return Ok(await Mediator.Send(new GetExerciseHistoryQuery(exerciseId)));
        }
    }
}
