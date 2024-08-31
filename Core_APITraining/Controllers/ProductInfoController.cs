using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core_APITraining.Models;

namespace Core_APITraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInfoController : ControllerBase
    {
        private readonly TestDataContext _context;

        public ProductInfoController(TestDataContext context)
        {
            _context = context;
        }

        // GET: api/ProductInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInfo>>> GetProductInfos()
        {
            return await _context.ProductInfos.ToListAsync();
        }

        // GET: api/ProductInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInfo>> GetProductInfo(int id)
        {
            var productInfo = await _context.ProductInfos.FindAsync(id);

            if (productInfo == null)
            {
                return NotFound();
            }

            return productInfo;
        }

        // PUT: api/ProductInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInfo(int id, ProductInfo productInfo)
        {
            if (id != productInfo.ProductRowId)
            {
                return BadRequest();
            }

            _context.Entry(productInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInfoExists(id))
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

        // POST: api/ProductInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInfo>> PostProductInfo(ProductInfo productInfo)
        {
            _context.ProductInfos.Add(productInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductInfo", new { id = productInfo.ProductRowId }, productInfo);
        }

        // DELETE: api/ProductInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInfo(int id)
        {
            var productInfo = await _context.ProductInfos.FindAsync(id);
            if (productInfo == null)
            {
                return NotFound();
            }

            _context.ProductInfos.Remove(productInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductInfoExists(int id)
        {
            return _context.ProductInfos.Any(e => e.ProductRowId == id);
        }
    }
}
