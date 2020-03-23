using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoronaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoronaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CronJobsController : ControllerBase
    {
        private readonly CoronaStatsDbContext coronaStatsDbContext;

        public CronJobsController(CoronaStatsDbContext coronaStatsDbContext)
        {
            this.coronaStatsDbContext = coronaStatsDbContext;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Call()
        {
            // call go link
            var dataOfStat = await GetCoronaDetailsFromGovLink();

            // check changes 
            if (dataOfStat == null)
            {
                return StatusCode(500, "link response is error");
            }

            var previousRecord = await UpdateStatChanges(dataOfStat);

            // send notification to firebase
            if (previousRecord.CasesCount != dataOfStat.local_total_cases)
            {
                var a = "";
            }

            // send response
            return Ok();
        }

        private async Task<CoronaRecord> UpdateStatChanges(Data data)
        {
            // get current data row using date            
            var previousRecord = await this.coronaStatsDbContext.CoronaRecords.OrderByDescending(o => o.RecordDate.Date).Take(1).FirstOrDefaultAsync();

            if (previousRecord.RecordDate.Date == DateTime.Now.Date)
            {
                // update
                if (previousRecord.CasesCount != data.local_total_cases)
                {
                    previousRecord.RecoverCount = data.local_recovered;
                    previousRecord.DeathCount = data.local_deaths;
                    previousRecord.CasesCount = data.local_total_cases;
                    previousRecord.SuspectCount = data.local_total_number_of_individuals_in_hospitals;
                    this.coronaStatsDbContext.CoronaRecords.Update(previousRecord);
                    await this.coronaStatsDbContext.SaveChangesAsync();
                }
            }
            else
            {
                // insert
                var newRecord = new CoronaRecord()
                {
                    RecordDate = DateTime.Now.Date,
                    CasesCount = data.local_total_cases,
                    RecoverCount = data.local_recovered,
                    DeathCount = data.local_deaths,
                    SuspectCount = data.local_total_number_of_individuals_in_hospitals
                };
                await this.coronaStatsDbContext.CoronaRecords.AddAsync(newRecord);
                await this.coronaStatsDbContext.SaveChangesAsync();
            }

            
            return previousRecord;
        }

        private async Task<Data> GetCoronaDetailsFromGovLink()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://hpb.health.gov.lk/api/get-current-statistical"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var govRes = JsonConvert.DeserializeObject<CoronaGovResponse>(apiResponse);
                        return govRes.data;
                    }
                }

                return null;
            }
        }
    }
}