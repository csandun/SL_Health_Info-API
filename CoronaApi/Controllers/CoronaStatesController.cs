using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronaStatesController : ControllerBase
    {
        private readonly CoronaStatsDbContext coronaStatsDbContext;
        public CoronaStatesController(CoronaStatsDbContext coronaStatsDbContext)
        {
            this.coronaStatsDbContext = coronaStatsDbContext;
        }

        [Route("{days:int}")]
        [HttpGet]
        public IActionResult Get([FromRoute] int days)
        {
            var list = this.coronaStatsDbContext.CoronaRecords
                .Where(o => o.RecordDate.Date >= (DateTime.Now.AddDays(-days).Date)).OrderBy(o => o.RecordDate.Date);
            return Ok(new { Records = list });
        }
    }
}