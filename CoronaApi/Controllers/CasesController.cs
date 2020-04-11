using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoronaApi.Dto;
using CoronaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoronaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly CoronaStatsDbContext coronaStatsDbContext;
        public CasesController(CoronaStatsDbContext coronaStatsDbContext)
        {
            this.coronaStatsDbContext = coronaStatsDbContext;
        }


        [Route("save")]
        [HttpGet]
        public async Task<IActionResult> Save()
        {
            // get gov latest id 
            var govLatestId = await this.GetGovLatestCaseId();

            // check get over latest gov id
            var latestCase = await coronaStatsDbContext.Cases.OrderByDescending(o => o.GovId).FirstOrDefaultAsync();
            var latestCaseGovId = 0;
            if (latestCase != null)
            {
                latestCaseGovId = latestCase.GovId;
            }

            if (govLatestId == latestCaseGovId)
            {
                return Ok("No changes");
            }

            // save new cases into over database
            for (int i = latestCaseGovId + 1; i <= govLatestId; i++)
            {
                var coronaCase = await GetCase(i);
                if (coronaCase != null)
                {
                    this.coronaStatsDbContext.Cases.Add(coronaCase);
                    await this.coronaStatsDbContext.SaveChangesAsync();
                }
            }

            return Ok($"gov latest ${govLatestId} and over latest ${latestCaseGovId}");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cases = await coronaStatsDbContext.Cases.OrderByDescending(o => o.GovId).ToListAsync();
            return Ok(cases);
        }


        private async Task<int?> GetGovLatestCaseId()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.covid-19.health.gov.lk/application/case/latest"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var govlatestId = int.Parse(apiResponse);
                        return govlatestId;
                    }
                }

                return null;
            }
        }

        private async Task<Case> GetCase(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.covid-19.health.gov.lk/application/case/" + id + "/en"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var govRes = JsonConvert.DeserializeObject<CaseDto>(apiResponse);

                        var coronacase = new Case()
                        {
                            GovId = int.Parse(govRes.id),
                            CaseNumber = int.Parse(govRes.caseNumber),
                            IsLocal = govRes.isLocal,
                            DetectedFrom = govRes.detectedFrom,
                            Local = govRes.local,
                            GovCreated = DateTime.Parse(govRes.created),
                            ReportedDate = DateTime.Parse(govRes.locations[0].date),
                            Area = govRes.locations[0].area,
                            Latitude = double.Parse(govRes.locations[0].latitude),
                            Longitude = double.Parse(govRes.locations[0].longitude)
                        };
                        return coronacase;
                    }
                }

                return null;
            }
        }

    }
}