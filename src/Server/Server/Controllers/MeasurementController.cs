using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Hangfire;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

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
        }

        static string connectionString = "broker.mqttdashboard.com"; // 192.168.3.3 
        string clientId = "clientId-qn6emilV3X";
        MqttClient client = new MqttClient(connectionString);


        // Wordt getriggered wanneer er een message gepublished wordt
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string payload = Encoding.UTF8.GetString(e.Message);
            SaveMeasurementInDB(payload);
        }

        private void SaveMeasurementInDB(string payload)
        {   
            // Wat er binnenkomt via MQTT
            // TAG{nr};ANCHOR{nr};{distance};{unix_timestamp}
            // TAG5;ANCHOR1;-4;1557475973
            string[] data = payload.Split(';');
            if (_context.Measurements.Any(a => a.Mac_Anchor == data[1]))
            {
                Measurement measure = _context.Measurements.Where(a => a.Mac_Anchor == data[1]).LastOrDefault();
                measure.Distance = int.Parse(data[2]);
                measure.Unix_Timestamp = data[3];
            }
            else
            {
                _context.Measurements.Add(
                    new Measurement()
                    {
                        Mac_Tag = data[0],
                        Mac_Anchor = data[1],
                        Distance = int.Parse(data[2]),
                        Unix_Timestamp = data[3]
                    } 
                );
                _context.SaveChanges();
            }
        }

        public void DoInBackground()
        {
            client.Connect(clientId);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            while (true)
            {

                client.Subscribe(new string[] { "LUWB/TAG5" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE});
                Thread.Sleep(200);
            }
        }
 
        [HttpPost]
        public ActionResult<Measurement> Create([FromBody]Measurement item)
        {
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
            BackgroundJob.Enqueue(() => DoInBackground());
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