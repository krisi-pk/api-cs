using Microsoft.AspNetCore.Mvc;
using MySecondProductApi.Data;
using MySecondProductApi.Models;

namespace MySecondProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductContext _context;
        public ProductController(ProductContext context) {
            _context = context;

            if (_context.Products.Count() == 0) {
                _context.Products.Add(new Product { Id = 1, Name = "Lipstick", Quantity = 3 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll() {
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id) {
            var product = _context.Products.Find(id);
            if (product == null) {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product) {
            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product) {
            var productToEdit = _context.Products.Find(id);
            if (productToEdit == null) {
                return NotFound();
            }

            productToEdit.Id = id;
            productToEdit.Name = product.Name;
            productToEdit.Quantity = product.Quantity;

            _context.Update(productToEdit);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var product = _context.Products.Find(id);
            if (product == null) { 
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
