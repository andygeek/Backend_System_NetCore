using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemBackend.Data;
using SystemBackend.Entities;
using SystemBackend.Web.Models;

namespace SystemBackend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DbContextSystemBackend _context;

        public ProductsController(DbContextSystemBackend context)
        {
            _context = context;
        }

        // GET: api/Products/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProductViewModel>> List()
        {
            var product = await _context.Products.Include(a => a.category).Include(a => a.brand).Include(a => a.unit).ToListAsync();

            return product.Select(a => new ProductViewModel
            {
                id = a.id,
                code = a.code,
                name = a.name,
                description = a.description,
                unitId = a.unitId,
                unitName = a.unit.name,
                categoryId = a.categoryId,
                categoryName = a.category.name,
                brandId = a.brandId,
                brandName = a.brand.name,
                cost = a.cost,
                price = a.price
            });
        }

        // POST: api/Products/create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product product = new Product
            {
                code = model.code,
                name = model.name,
                brandId = model.brandId,
                categoryId = model.categoryId,
                description = model.description
            };
            _context.Products.Add(product);
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

        // PUT: api/Products/update/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id < 0)
            {
                return BadRequest();
            }

            var product = await _context.Products.FirstOrDefaultAsync(c => c.id == model.id);

            if (product == null)
            {
                return NotFound();
            }

            product.code = model.code;
            product.name = model.name;
            product.categoryId = model.categoryId;
            product.brandId = model.brandId;
            
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

        // DELETE: api/Products/delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                BadRequest();
            }
            return Ok(product);
        }

        // GET: api/Products/Search_code/<code>
        [HttpGet("[action]/{code}")]
        public async Task<IEnumerable<ProductViewModel>> Search_code([FromRoute] string code)
        {
            var product = await _context.Products.Include(a => a.category).Include(a => a.brand).ToListAsync();

            return product.Where(a => a.code == code).Select(a => new ProductViewModel
            {
                id = a.id,
                code = a.code,
                name = a.name,
                brandId = a.brandId,
                brandName = a.brand.name,
                categoryId = a.categoryId,
                categoryName = a.category.name,
                description = a.description
            });
        }



        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }
    }
}