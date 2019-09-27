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
        public List<Tag> GetTagsParam(string mac, string description,
                                            string sortBy, string direction = "asc",
                                            int pageSize = 5, int pageNumber = 0)
        {
            IQueryable<Tag> query = context.Tags;
            if (!string.IsNullOrEmpty(mac))
                query = query.Where(b => b.Mac == mac);
            if (!string.IsNullOrEmpty(description))
                query = query.Where(b => b.Description == description);

            if (string.IsNullOrEmpty(sortBy)) sortBy = "default";
            switch (sortBy.ToLower())
            {
                case "mac":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Mac);
                    else
                        query = query.OrderByDescending(b => b.Mac);
                    break;
                case "description":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Description);
                    else
                        query = query.OrderByDescending(b => b.Description);
                    break;
                case "xpos":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.XPos);
                    else
                        query = query.OrderByDescending(b => b.XPos);
                    break;
                case "ypos":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.YPos);
                    else
                        query = query.OrderByDescending(b => b.YPos);
                    break;
                default:
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Id);
                    else
                        query = query.OrderByDescending(b => b.Id);
                    break;
            }
            query = query.Skip(pageNumber * pageSize);
            if (pageSize > 25) pageSize = 25;
            if (pageSize <= 0) pageSize = 5;
            query = query.Take(pageSize);
            return query.ToList();
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