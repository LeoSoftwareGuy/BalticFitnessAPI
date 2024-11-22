using Microsoft.AspNetCore.Mvc;
using Application.MonthlyStatistics;
using Application.MonthlyStatistics.Queries.GetMonthlyStats;
using Application.MonthlyStatistics.Queries.GetSummaryInfoBasedOnFilter;

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
            return Ok(await Mediator.Send(new GetSummaryInfoBasedOnFilterQuery() { Filter = filterBy }));
        }
    }
}
