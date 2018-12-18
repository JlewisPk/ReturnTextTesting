using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnTextsController : ControllerBase
    {
        private readonly APIContext _context;

        public ReturnTextsController(APIContext context)
        {
            _context = context;
        }

        // GET: api/ReturnTexts
        [HttpGet]
        public IEnumerable<ReturnText> GetReturnText()
        {
            return _context.ReturnText;
        }

        // GET: api/ReturnTexts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReturnText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var returnText = await _context.ReturnText.FindAsync(id);

            if (returnText == null)
            {
                return NotFound();
            }

            return Ok(returnText);
        }
        // GET: api/ReturnTexts/5
        [HttpPost("/getText/{id}")]
        [Produces("application/json")]
        public async Task<ContentResult> GetReturnTextUsingEmail([FromRoute] int id, SlashCom slashCom)
        {
            System.Diagnostics.Debug.WriteLine("=================================================");
            System.Diagnostics.Debug.WriteLine(slashCom.text);
            System.Diagnostics.Debug.WriteLine("=================================================");
            if (!ModelState.IsValid)
            {
                return Content("You need to pass Email address too!");
            }

            var returnText = await _context.ReturnText.FindAsync(id);

            if (returnText == null)
            {
                return Content("No given Email exist!");
            }

            return Content(returnText.Text);
        }
        // PUT: api/ReturnTexts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReturnText([FromRoute] int id, [FromBody] ReturnText returnText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != returnText.Id)
            {
                return BadRequest();
            }

            _context.Entry(returnText).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnTextExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReturnTexts
        [HttpPost]
        public async Task<IActionResult> PostReturnText([FromBody] ReturnText returnText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ReturnText.Add(returnText);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReturnText", new { id = returnText.Id }, returnText);
        }

        // DELETE: api/ReturnTexts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReturnText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var returnText = await _context.ReturnText.FindAsync(id);
            if (returnText == null)
            {
                return NotFound();
            }

            _context.ReturnText.Remove(returnText);
            await _context.SaveChangesAsync();

            return Ok(returnText);
        }

        private bool ReturnTextExists(int id)
        {
            return _context.ReturnText.Any(e => e.Id == id);
        }
    }
}