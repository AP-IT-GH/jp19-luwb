using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Server.Algorithm;

namespace Server.Controllers
{
    [Route("api/map")]
    [EnableCors("AllowAllMethods")]
    public class MapController : Controller
    {
        private List<Coordinate> coordinates;
        private readonly DatabaseContext _context;
        public MapController(DatabaseContext context)
        {
             
           _context = context;
            //if (_context.Coordinates.Count() == 0)
            //{
            //    _context.Coordinates.Add(new Coordinate { TagId = "1", XPos = 123132132, YPos = 100, Stroke = 5 });
            //    _context.Coordinates.Add(new Coordinate { TagId = "2", XPos = 20, YPos = 150, Stroke = 5 });
            //    _context.Coordinates.Add(new Coordinate { TagId = "3", XPos = 150, YPos = 50, Stroke = 5 });
            //    _context.SaveChanges();
            //}
        }




        [Route("{userId}/{mapId}")]
        [HttpGet]
        public IActionResult GetCoordinates(int userId, long mapId)
        {
            coordinates = new List<Coordinate>();

            var map = _context.Maps.Find(mapId);
             
            
            //lijst van tags voor bepaalde user
            IQueryable<Tag> tag_query = _context.Tags;
            //var tags = tag_query.Where(d => d.User.Id == userId);
            List<Tag> tags = _context.Tags
                           .Where(d => d.User.Id == userId)
                           .Where(m => m.Map.Id == mapId)
                           .Select(e => new Tag
                           {
                               Id = e.Id,
                               Mac = e.Mac,
                               Description = e.Description
                           })
                           .ToList();

            IQueryable<Anchor> anchor_query = _context.Anchors;
            List<Anchor> anchors = anchor_query
                                      .Where(d => d.User.Id == userId)
                                      .Where(m => m.Map.Id == mapId)
                                      .Select(e => new Anchor
                                      {
                                          X_Pos = e.X_Pos,
                                          Y_Pos = e.Y_Pos,
                                          Mac = e.Mac
                                      })
                                      .ToList();

            //return Ok(anchors);
            
            foreach (var tag in tags)
            {
                List<Data> dataList = new List<Data>();
                foreach (var anchor in anchors)
                {
                    IQueryable<Measurement> query = _context.Measurements;
                    List<Measurement> measurements = query.Where(a => a.Mac_Anchor == anchor.Mac)
                                    .Where(t => t.Mac_Tag == tag.Mac)
                                    .ToList();

                    if(measurements.Count > 0)
                    {
                        var measurement = query.Where(a => a.Mac_Anchor == anchor.Mac)
                                .Where(t => t.Mac_Tag == tag.Mac)
                                .OrderBy(o => o.Id)
                                .Last();

                        dataList.Add(new Data
                        {
                            X_Pos = anchor.X_Pos,
                            Y_Pos = anchor.Y_Pos,
                            Distance = measurement.Distance
                        });
                    }
                }
                if (dataList.Count() > 0)
                {


                    double[] pos = Algorithm.Algorithm.Calculate(dataList);

                    //int xPos = (int)((pos[0]) / (map.Width / 100));
                    //int yPos = (int)((pos[1]) / (map.Length / 100));

                    double widthFactor = map.Width / 100.0;
                    double heightFactor = map.Length / 100.0;

                    double xPos = pos[0] / widthFactor;
                    double yPos = pos[1] / heightFactor;

                    coordinates.Add(new Coordinate
                    {
                        Tag = tag,
                        X_Pos = xPos,
                        Y_Pos = yPos
                    });
                }

            }
            ReturnMap returnMap = new ReturnMap
            {
                Picture = map.Picture,
                Coordinates = coordinates
            };
            return Ok(returnMap);
           
        }


        [HttpPost]
        [Route("{userId}")]
        public IActionResult AddMap([FromBody] Map map, int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
                return NotFound();
            //oldMap.Map = user.Map;

            map.User = user;
            _context.Maps.Add(map);
            if (_context.SaveChanges() < 0)
                return NotFound();
            return Ok(map);
        }

        [HttpPut]
        [Route("{mapId}")]
        public IActionResult UpdateMap([FromBody] Map map, long mapId)
        {
            var newMap = _context.Maps.Find(mapId);

            if (map.Picture != null)
                newMap.Picture = map.Picture;
            if (map.Length != 0)
                newMap.Length = map.Length;
            if (map.Width != 0)
                newMap.Width = map.Width;

            if (_context.SaveChanges() < 0)
                return NotFound(newMap);
            return Ok(newMap);


        }


        public IActionResult Index()
        {
            return View();
        }
    }

    public class ReturnMap
    {
        public string Picture { get; set; }
        public List<Coordinate> Coordinates { get; set; }
    }
    public class Coordinate
    {
        public Tag Tag { get; set; }
        public double X_Pos { get; set; }
        public double Y_Pos { get; set; }
        public bool Status { get { return true; } }
        public int Stroke { get { return 5; } } //moet van Jens
    }

    public class Data
    {
        public double Distance { get; set; }
        public double X_Pos { get; set; }
        public double Y_Pos { get; set; }
    }

    
}