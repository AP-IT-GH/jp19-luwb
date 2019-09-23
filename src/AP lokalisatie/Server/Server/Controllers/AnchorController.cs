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
        public ActionResult<List<Anchor>> GetAll()
        {
            return context.Anchors.ToList();
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
            var anchorId = anchor.Id;
            if (context.Anchors.Find(anchorId) == null)
                return NotFound();
            context.Anchors.Update(anchor);
            context.SaveChanges();
            return Ok(anchor);
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