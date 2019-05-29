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
    [Route("api/tags")]
    [ApiController]
    [EnableCors("AllowAllMethods")]
    public class TagController : Controller
    {
        private readonly DatabaseContext _context;
        public TagController(DatabaseContext context)
        {
            _context = context;
            //if (_context.Tags.Count() == 0)
            //{
            //    _context.Tags.Add(new Tag { Mac = "11:11:11:11" });
            //    _context.Tags.Add(new Tag { Mac = "22:22:22:22" });
            //    _context.SaveChanges();
            //}
        }

        [HttpPost]
        [Route("{userId}/{mapId}")]
        public IActionResult Create(Tag item, int userId, long mapId)
        {
            User user = _context.Users.Find(userId);
            Map map = _context.Maps.Find(mapId);
            item.User = user;
            item.Map = map;
            _context.Tags.Add(item);

            if (_context.SaveChanges() > 0)
                return Ok(item);

            return NotFound();
        }

        //// GET: api/<controller>
        //[HttpGet]
        //public ActionResult<List<Tag>> GetAll()
        //{
        //    return _context.Tags.ToList();
        //}
        [Route("{userId}")]
        [HttpGet]
        public IActionResult GetTags(int userId)
        {
            var tags = _context.Tags.Where(m => m.User.Id == userId).ToList();
            if (tags != null)
                return Ok(tags);
            return NotFound();
        }

        [Route("{userId}/{tagId}")]
        [HttpGet]
        public IActionResult GetTag(int userId, long tagId)
        {
            var tag = _context.Tags.Where(d => d.User.Id == userId).Where(m => m.Id == tagId).First();
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [Route("{tagId}")]
        [HttpPut]
        public IActionResult Put([FromBody] Tag updateTag, long tagId)
        {
            var tag = _context.Tags.Find(tagId);
            if (tag == null)
                return NotFound();

            tag.Description = updateTag.Description;
            _context.SaveChanges();
            return Ok(tag);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var tag = _context.Tags.Find(id);
            if (tag == null)
                return NotFound();
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Ok();
        }
    }

    public class DetailTag
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Mac { get; set; }
        //public List<>
    }
}