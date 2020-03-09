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
    public class BrandsController : ControllerBase
    {
        private readonly DbContextSystemBackend _context;

        public BrandsController(DbContextSystemBackend context)
        {
            _context = context;
        }

        // GET: api/Brands/ListBrands
        [HttpGet("[action]")]
        public async Task <IEnumerable<BrandViewModel>> List()
        {
            var brand = await _context.Brands.ToListAsync();
            return brand.Select(c => new BrandViewModel
            {
                id = c.id,
                name = c.name
            });
        }

        // GET: api/Brands/Shoow/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Shoow([FromRoute] int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(new BrandViewModel {
                id = brand.id,
                name = brand.name
            });
        }

        // PUT: api/Brands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand([FromRoute] int id, [FromBody] Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brand.id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands/create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] BrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Brand brand = new Brand
            {
                name = model.name
            };
            _context.Brands.Add(brand);
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

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return Ok(brand);
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.id == id);
        }
    }
}