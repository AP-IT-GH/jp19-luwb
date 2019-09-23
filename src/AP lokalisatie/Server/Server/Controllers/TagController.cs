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
        private readonly DatabaseContext context;
        public TagController(DatabaseContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public ActionResult<List<Tag>> GetAll()
        {
            return context.Tags.ToList();
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<Tag> GetTag(long id)
        {
            var tag = context.Tags.Find(id);
            if (tag == null)
                return NotFound();
            return tag;
        }

        [HttpPost]
        public ActionResult<Tag> AddTag([FromBody]Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();
            return Created("", tag);
        }

        [HttpPut]
        public ActionResult<Tag> UpdateTag([FromBody]Tag tag)
        {
            var tagId = tag.Id;
            if (context.Tags.Find(tagId) == null)
                return NotFound();
            context.Tags.Update(tag);
            context.SaveChanges();
            return Ok(tag);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var tag = context.Tags.Find(id);
            if (tag == null)
                return NotFound();
            context.Tags.Remove(tag);
            context.SaveChanges();
            return Ok();
        }
    }
}