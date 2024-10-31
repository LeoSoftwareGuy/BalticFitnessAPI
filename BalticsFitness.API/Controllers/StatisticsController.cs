using Microsoft.AspNetCore.Mvc;
using Application.MonthlyStatistics;
using Application.MonthlyStatistics.Queries;

namespace BalticsFitness.API.Controllers
{
    public class StatisticsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<StatResults>> GetMonthlyStats()
        {
            return Ok(await Mediator.Send(new GetMonthlyStatsQuery()));
        }
    }
}
