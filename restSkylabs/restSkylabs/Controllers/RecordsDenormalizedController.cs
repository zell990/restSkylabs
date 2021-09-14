using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoweredSoft.DynamicLinq;
using RecordsCsvResults.Models;
using restSkylabs.DAL;
using restSkylabs.Model;

namespace WebAPISkylabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsDenormalizedController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<RecordsDenormalizedController> _logger;

        public RecordsDenormalizedController(ApplicationDBContext context, ILogger<RecordsDenormalizedController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/RecordsDenormalized
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Records>>> GetRecords()
        {
            _logger.LogInformation("Return records");
            return await _context.Records.Include(w => w.Workclasses)
                                         .Include(w => w.Relationships)
                                         .Include(w => w.Sexes)
                                         .Include(w => w.Races)
                                         .Include(w => w.Occupations)
                                         .Include(w => w.Marital_Statuses)
                                         .Include(w => w.Education_Levels)
                                         .Include(w => w.Countries).ToListAsync();
        }

        // GET: api/RecordsDenormalized/pagination
        //https://localhost:5001/api/RecordsDenormalized/pagination?offset=2&count=5
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<Records>>> GetRecords(int offset, int count)
        {
            _logger.LogInformation("Return records");
            _logger.LogInformation(offset.ToString());
            _logger.LogInformation(count.ToString());
            return await _context.Records.Include(w => w.Workclasses)
                                         .Include(w => w.Relationships)
                                         .Include(w => w.Sexes)
                                         .Include(w => w.Races)
                                         .Include(w => w.Occupations)
                                         .Include(w => w.Marital_Statuses)
                                         .Include(w => w.Education_Levels)
                                         .Include(w => w.Countries)
                                         .OrderBy(w => w.ID_Records)
                                         .Skip(offset)
                                         .Take(count).ToListAsync();
        }

        // GET: api/RecordsDenormalized/downloadCSV
        [HttpGet("downloadCSV")]
        public IActionResult Download()
        {
            IEnumerable<Records> recordsData = _context.Records.Include(w => w.Workclasses)
                                         .Include(w => w.Relationships)
                                         .Include(w => w.Sexes)
                                         .Include(w => w.Races)
                                         .Include(w => w.Occupations)
                                         .Include(w => w.Marital_Statuses)
                                         .Include(w => w.Education_Levels)
                                         .Include(w => w.Countries).ToList();


            string fileDownloadName = "records.csv";
            return new RecordsCsvResult(recordsData, fileDownloadName);
        }

        // GET: api/RecordsDenormalized/statistics
        //https://localhost:5001/api/RecordsDenormalized/statistics?aggregationType=Age&aggregationValue=30
        [HttpGet("statistics")]
        public ActionResult<RecordsStatistics> Statistics(string aggregationType, int aggregationValue)
        {

            IEnumerable<Records> recordsData = _context.Records.Include(w => w.Workclasses)
                                         .Include(w => w.Relationships)
                                         .Include(w => w.Sexes)
                                         .Include(w => w.Races)
                                         .Include(w => w.Occupations)
                                         .Include(w => w.Marital_Statuses)
                                         .Include(w => w.Education_Levels)
                                         .Include(w => w.Countries).ToList();

                switch (aggregationType.ToLower())
                {
                    case "age":
                    recordsData = recordsData.Where(w => w.Age == aggregationValue);
                        break;
                    case "education_level_id":
                    recordsData = recordsData.Where(w => w.Education_Level_ID == aggregationValue);
                        break;
                    case "occupation_id":
                    recordsData = recordsData.Where(w => w.Occupation_ID == aggregationValue);
                        break;
                }

            RecordsStatistics statistics = new RecordsStatistics();

            statistics.AggregationType = aggregationType;
            statistics.AggregationFilter = aggregationValue;
            statistics.Capital_Gain_Sum = recordsData.Sum(w => w.Capital_Gain); 
            statistics.Capital_Gain_Avg = recordsData.Select(w => w.Capital_Gain).Average(); 
            statistics.Capital_Loss_Avg = recordsData.Select(w => w.Capital_Loss).Average();
            statistics.Capital_Loss_Sum = recordsData.Sum(w => w.Capital_Loss);
            statistics.Over_50k_count = recordsData.Where(w => w.Over_50k == true).Count();
            statistics.Under_50k_count = recordsData.Where(w => w.Over_50k == false).Count();

            return statistics;
        }

    }
}
