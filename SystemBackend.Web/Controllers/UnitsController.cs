using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemBackend.Data;
using SystemBackend.Entities;
using SystemBackend.Web.Models;

namespace SystemBackend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly DbContextSystemBackend _context;

        public UnitsController(DbContextSystemBackend context)
        {
            _context = context;
        }

        // GET: api/Units/list
        [HttpGet("[action]")]
        public async Task<IEnumerable<UnitViewModel>> List()
        {
            var unit = await _context.Units.ToListAsync();
            return unit.Select(c => new UnitViewModel
            {
                id = c.id,
                name = c.name
            });
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return Ok(unit);
        }

        // PUT: api/units/update/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UnitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id < 0)
            {
                return BadRequest();
            }

            var unit = await _context.Units.FirstOrDefaultAsync(c => c.id == model.id);

            if (unit == null)
            {
                return NotFound();
            }

            unit.name = model.name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Units
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] UnitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Unit unit = new Unit
            {
                name = model.name
            };

            _context.Units.Add(unit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/units/delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                BadRequest();
            }
            return Ok();
        }

        private bool UnitExists(int id)
        {
            return _context.Units.Any(e => e.id == id);
        }
    }
}