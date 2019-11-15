using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/measurements")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MeasurementController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<Measurement> Create([FromBody]Measurement item)
        {
            Measurement measure = _context.Measurements.Where(a => a.Mac_Anchor == item.Mac_Anchor).Where(a => a.Mac_Tag == item.Mac_Tag).FirstOrDefault();
            if (measure != null)
            {
                measure.Distance = item.Distance;
                measure.Unix_Timestamp = item.Unix_Timestamp;
            }
            else
            {
                _context.Measurements.Add(item);
                _context.SaveChanges();
            }

            if (_context.SaveChanges() > 0)
                return Ok();

            return NotFound();
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Measurement>> GetAll()
        {
            return _context.Measurements.ToList();
        }

        [Route("{mac}")]
        [HttpGet]
        public ActionResult GetMeasurement(string mac)
        {
            IQueryable<Measurement> query = _context.Measurements;
            var data = query.Where(d => d.Mac_Tag == mac);
            long id = data.Max<Measurement>(x => x.Id);

            Measurement m = _context.Measurements.Find(id);
            if (m == null)
                return NotFound();

            return Ok();
        }
    }
}