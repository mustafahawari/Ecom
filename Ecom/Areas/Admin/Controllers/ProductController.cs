using Ecom.DataAccess.Repository;
using Ecom.Models;
using Ecom.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecom.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        // GET: HomeController
        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAll(null).ToList();
            return View(products);
        }

        // GET: HomeController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Upsert(int? productId)
        {
            IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll(null).ToList()
                .Select(u => new SelectListItem(text: u.Name, value: u.CategoryId.ToString()));

            ProductVM productVM = new() { CategoyList = categoryList, Product = new Product() };
            if(productId == null && productId != 0)
            {
                return View(productVM);

            } else
            {
                productVM.Product = _productRepository.Get(u => u.ProductId == productId);
                return View(productVM);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if(file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productImagePath = Path.Combine(wwwRootPath, @"images\product");

                        if(!string.IsNullOrEmpty(obj.Product.ImageUrl)) 
                        {
                            var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                            if(System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        using (var fileStream = new FileStream(Path.Combine(productImagePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        obj.Product.ImageUrl = @"\images\product\" + fileName;
                    }
                    if(obj.Product.ProductId == 0)
                    {
                        _productRepository.Add(obj.Product);
                    } else
                    {
                        _productRepository.Update(obj.Product);
                    }
                    string notificationText = obj.Product.ProductId == 0 ? "Created " : "Updated ";
                    _productRepository.Save();
                    TempData["success"] = "Product " + notificationText +  "successfully";
                    return RedirectToAction(nameof(Index));

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product? product = _productRepository.Get(product => product.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: HomeController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                Product? product = _productRepository.Get(p => p.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
                _productRepository.Remove(product);
                _productRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
