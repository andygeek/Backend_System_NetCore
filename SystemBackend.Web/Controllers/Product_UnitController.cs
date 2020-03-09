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
    public class Product_UnitController : ControllerBase
    {
        private readonly DbContextSystemBackend _context;

        public Product_UnitController(DbContextSystemBackend context)
        {
            _context = context;
        }

        // GET: api/Product_Unit
        [HttpGet]
        public IEnumerable<Product_Unit> Getproduct_Units()
        {
            return _context.product_Units;
        }

        // GET: api/Product_Unit/<product id>
        [HttpGet("[action]/{id_product}")]
        public async Task<IEnumerable<Product_UnitViewModel>> GetProductID([FromRoute] int id_product)
        {
            var product_unit = await _context.product_Units.Include(a => a.product).Include(a => a.unit).ToListAsync();

            return product_unit.Where(a => a.product.id == id_product).Select(a => new Product_UnitViewModel
            {
                productId = a.product.id,
                unitId = a.unit.id,
                unitName = a.unit.name,
                cost = a.cost,
                price = a.price
                
            });
        }

        // PUT: api/Product_Unit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct_Unit([FromRoute] int id, [FromBody] Product_Unit product_Unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_Unit.unitId)
            {
                return BadRequest();
            }

            _context.Entry(product_Unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_UnitExists(id))
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

        // POST: api/Product_Unit
        [HttpPost]
        public async Task<IActionResult> PostProduct_Unit([FromBody] Product_Unit product_Unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.product_Units.Add(product_Unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct_Unit", new { id = product_Unit.unitId }, product_Unit);
        }


        // POST: api/Product_Unit/create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Product_UnitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product_Unit product_unit = new Product_Unit
            {
                productId = model.productId,
                unitId = model.unitId,
                cost = model.cost,
                price = model.price
            };
            _context.product_Units.Add(product_unit);
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


        // DELETE: api/Product_Unit/delete/5
        [HttpDelete("[action]/{id_product}/{id_unit}")]
        public async Task<IActionResult> Delete([FromRoute] int id_unit, [FromRoute] int id_product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product_Unit = await _context.product_Units.FindAsync(id_product, id_unit);
            if (product_Unit == null)
            {
                return NotFound();
            }

            _context.product_Units.Remove(product_Unit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                BadRequest();
            }
            return Ok(product_Unit);
        }

        private bool Product_UnitExists(int id)
        {
            return _context.product_Units.Any(e => e.unitId == id);
        }
    }
}