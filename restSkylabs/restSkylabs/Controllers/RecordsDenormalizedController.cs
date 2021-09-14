using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                                         .Skip(offset)
                                         .Take(count).ToListAsync();
        }


        [HttpGet("downloadCSV")]
        public IActionResult Download()
        {
            List<Records> recordsData = _context.Records.Include(w => w.Workclasses)
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

        // GET: api/RecordsDenormalized/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Records>> GetRecords(int id)
        //{
        //    var records = await _context.Records.FindAsync(id);

        //    if (records == null)
        //    {
        //        return NotFound();
        //    }

        //    return records;
        //}

        // PUT: api/RecordsDenormalized/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRecords(int id, Records records)
        //{
        //    if (id != records.ID_Records)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(records).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RecordsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/RecordsDenormalized
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Records>> PostRecords(Records records)
        //{
        //    _context.Records.Add(records);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRecords", new { id = records.ID_Records }, records);
        //}

        //// DELETE: api/RecordsDenormalized/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Records>> DeleteRecords(int id)
        //{
        //    var records = await _context.Records.FindAsync(id);
        //    if (records == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Records.Remove(records);
        //    await _context.SaveChangesAsync();

        //    return records;
        //}

        //private bool RecordsExists(int id)
        //{
        //    return _context.Records.Any(e => e.ID_Records == id);
        //}
    }
}
