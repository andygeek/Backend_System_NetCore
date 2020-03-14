﻿using System;
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
    public class CategoriesController : ControllerBase
    {
        private readonly DbContextSystemBackend _context;

        public CategoriesController(DbContextSystemBackend context)
        {
            _context = context;
        }

        // GET: api/Categories/ListCategories
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryViewModel>> List()
        {
            var category = await _context.Categories.ToListAsync();
            return category.Select(c => new CategoryViewModel
            {
                id = c.id,
                name = c.name
            });
        }

        // GET: api/Categories/Shoow/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Shoow([FromRoute] int id)
        {

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryViewModel
            {
                id = category.id,
                name = category.name
            });
        }

        // PUT: api/categories/update/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id < 0)
            {
                return BadRequest();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.id == model.id);

            if (category == null)
            {
                return NotFound();
            }

            category.name = model.name;

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

        // POST: api/categories/create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category category = new Category
            {
                name = model.name
            };
            _context.Categories.Add(category);
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

        // DELETE: api/categories/delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
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

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.id == id);
        }
    }
}