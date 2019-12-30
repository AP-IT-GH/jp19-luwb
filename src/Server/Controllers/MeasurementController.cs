using System;
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
        public ActionResult<Measurement> CreateMeasurement([FromBody]Measurement item)
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

        [Route("{tagName}/{anchorName}/{distance}/{time}")]
        [HttpPut]
        public ActionResult<Measurement> UpdateMeasurement(string tagName, string anchorName, int distance, string time)
        {
            // TAG5;ANCHOR1;-4;1557475973
            // Verglijken mac van de anchors en opslaan in measure indien ze overeenkomen
            try
            {
                Measurement measure = _context.Measurements
                                        .Where(a => a.Mac_Anchor == anchorName)
                                        .Where(a => a.Mac_Tag == tagName)
                                        .FirstOrDefault();
                if (measure != null)
                {
                    // Opslaan van afstand en timestamp
                    measure.Distance = distance;
                    measure.Unix_Timestamp = time;
                }
                // Indien de mac anchor niet gevonden wordt, word er een nieuw object aangemaakt
                else
                {
                    _context.Measurements.Add(
                        new Measurement()
                        {
                            Mac_Tag = tagName,
                            Mac_Anchor = anchorName,
                            Distance = distance,
                            Unix_Timestamp = time
                        }
                    );
                }
                // Alle measurements zoeken van de meegegeven mac tag
                var measurements = _context.Measurements.Where(a => a.Mac_Tag == tagName).ToList();
                string firstMeasurementTimestamp = measurements[0].Unix_Timestamp;
                bool checkAllMeasurements = false;
                // Controleren of alle measurements van hetzelfde tijdstip zijn
                foreach (var measurement in measurements)
                {
                    if (measurement.Unix_Timestamp != firstMeasurementTimestamp)
                        checkAllMeasurements = true;
                    if (checkAllMeasurements == true) break;
                }
                // Indien alle measurements van hetzelfde tijdstip zijn, wordt de locatie van de tag berekend
                List<Data> dataList = new List<Data>();
                if (!checkAllMeasurements)
                {
                    foreach (var measurement in measurements)
                    {
                        string name = measurement.Mac_Anchor;
                        Anchor anchor = _context.Anchors.Where(a => a.Mac == name).FirstOrDefault();
                        if (anchor != null)
                            dataList.Add(new Data { Distance = measurement.Distance, X_Pos = anchor.XPos, Y_Pos = anchor.YPos });
                    }
                    // Berekenen van de locatie van de tag
                    double[] pos = Algorithm.Algorithm.Calculate(dataList);
                    // Opslaan van de locatie van de tag
                    Tag tag = _context.Tags.Where(a => a.Mac == tagName).FirstOrDefault();
                    if (tag != null)
                    {
                        tag.XPos = Convert.ToInt32(pos[0]);
                        tag.YPos = Convert.ToInt32(pos[1]);
                        _context.Tags.Update(tag);
                    }
                }
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e){
                return NotFound(e);
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Measurement>> GetAllMeasurements()
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