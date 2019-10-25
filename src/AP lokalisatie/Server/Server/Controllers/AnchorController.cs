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
    [Route("api/anchors")]
    [ApiController]
    [EnableCors("AllowAllMethods")]
    public class AnchorController : Controller
    {
        private readonly DatabaseContext context;
        public AnchorController(DatabaseContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public List<Anchor> GetAnchorsParam(string mac, string description,
                                            string sortBy, string direction = "asc",
                                            int pageSize = 5, int pageNumber = 0)
        {
            IQueryable<Anchor> query = context.Anchors;
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
        public ActionResult<Anchor> GetAnchor(long id)
        {
            var anchor = context.Anchors.Find(id);
            if (anchor == null)
                return NotFound();
            return anchor;
        }

        [HttpPost]
        public ActionResult<Anchor> AddAnchor([FromBody]Anchor anchor)
        {
            context.Anchors.Add(anchor);
            context.SaveChanges();
            return Created("", anchor);
        }

        [HttpPut]
        public ActionResult<Anchor> UpdateTag([FromBody]Anchor anchor)
        {
            //var anchorId = anchor.Id;
            //if (context.Anchors.Find(anchor.Id) == null)
            //    return NotFound();
            try
            {
                context.Anchors.Update(anchor);
                context.SaveChanges();
                return Ok(anchor);
            }
            catch{}
            return NotFound();

        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var anchor = context.Anchors.Find(id);
            if (anchor == null)
                return NotFound();
            context.Anchors.Remove(anchor);
            context.SaveChanges();
            return Ok();

        }
    }
}