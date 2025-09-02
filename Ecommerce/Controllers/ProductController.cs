using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>();

        public IActionResult Index()
        {
            return View(_products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {

            if (string.IsNullOrEmpty(product.Name) || product.Name.Length < 3 || product.Name.Length > 100)
            {
                ModelState.AddModelError("Name", "O nome do produto tem que ter entre 3 e 100 letras.");
            }

            if (product.Price < 0)
            {
                ModelState.AddModelError("Price", "O preço do produto tem que ser maior ou igual a zero.");
            }

            if (product.Stock < 0)
            {
                ModelState.AddModelError("Stock", "O estoque do produto tem que ser maior ou igual a zero.");
            }

            if (ModelState.IsValid)
            {

                product.ProductID = _products.Select(a => a.ProductID).DefaultIfEmpty(0).Max() + 1;
                _products.Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public IActionResult Edit(int id)
        {



            var product = _products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
           
            var existingProduct = _products.FirstOrDefault(p => p.ProductID == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(product.Name)  || product.Name.Length > 100 || product.Name.Length < 3)
            {
                ModelState.AddModelError("Name", "O nome do produto tem que ter entre 3 e 100 caracteres.");
            }

            if (product.Price < 0)
            {
                ModelState.AddModelError("Price", "O preço do produto tem que ser maior ou igual a zero.");
            }

            if (product.Stock < 0)
            {
                ModelState.AddModelError("Stock", "O estoque do produto tem que ser maior ou igual a zero.");
            }


            if (product.IsActive && product.Stock == 0)
            {
                ModelState.AddModelError("Stock", "Um produto ativo não pode ter estoque igual a  zero.");
            }

            if (ModelState.IsValid)
            {
              
                _products.Remove(existingProduct);
                _products.Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return RedirectToAction("Index");
        }

        
      
}
}