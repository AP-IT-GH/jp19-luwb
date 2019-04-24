
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/measurements")]
    [ApiController]
    [EnableCors("AllowAllMethods")]
    public class MeasurementController : Controller
    {
        private readonly DatabaseContext _context;
        public MeasurementController(DatabaseContext context)
        {
            _context = context;
            //if (_context.Measurements.Count() == 0)
            //{
            //    _context.Measurements.Add(new Measurements { AnchorMac = "11:11:11:11", TagMac = "11:11:11:11", Distance = 2.31 });
            //    _context.Measurements.Add(new Measurements { AnchorMac = "22:22:22:22", TagMac = "22:22:22:22", Distance = 4.25 });
            //    _context.SaveChanges();
            //}
        }

        [HttpPost]
        public ActionResult<Measurement> Create([FromBody]Measurement item)
        {
            //IQueryable<Measurement> query = _context.Measurements;
            //var data = query.Where(a => a.Mac_Anchor == item.Mac_Anchor).Where(t => t.Mac_Tag == item.Mac_Tag);
            //long id = data.Max<Measurement>(x => x.Id);

            //Measurement m = _context.Measurements.Find(id);

            //if (item.Distance == m.Distance)
            //{
            //    _context.Measurements.Remove(m);
            //}
            //_context.Measurements.Add(item);

           if(_context.Measurements.Any(a => a.Mac_Anchor == item.Mac_Anchor))
            {
                Measurement measure = _context.Measurements.Where(a => a.Mac_Anchor == item.Mac_Anchor).LastOrDefault();
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

            return Ok(m);
        }



    }
}