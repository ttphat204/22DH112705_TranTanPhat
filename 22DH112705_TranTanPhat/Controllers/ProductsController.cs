using _22DH112705_TranTanPhat.Data; // DbContext và Product
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _22DH112705_TranTanPhat.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SportProductContext _context;

        public ProductsController(SportProductContext context)
        {
            _context = context;
        }

        // GET: Products/FirstProduct
        public async Task<IActionResult> Index()
        {
            var product = await _context.Products.OrderBy(p => p.ProductId).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new List<string> { "Vợt", "Bóng", "Cầu", "Đệm", "Quần áo" };
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,NamePro,DecriptionPro,Category,Price,ImagePro,ManufacturingDate")] Product product)
        {
            var validCategories = new List<string> { "Vợt", "Bóng", "Cầu", "Đệm", "Quần áo" };

            // Kiểm tra danh mục
            if (string.IsNullOrEmpty(product.Category) || !validCategories.Contains(product.Category))
            {
                ModelState.AddModelError("Category", "Danh mục không hợp lệ. Chỉ chấp nhận: Vợt, Bóng, Cầu, Đệm, Quần áo.");
            }

            // Kiểm tra ngày sản xuất
            if (product.ManufacturingDate >= DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("ManufacturingDate", "Ngày sản xuất phải nhỏ hơn ngày hiện tại.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = validCategories;
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new List<string> { "Vợt", "Bóng", "Cầu", "Đệm", "Quần áo" };
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,NamePro,DecriptionPro,Category,Price,ImagePro,ManufacturingDate")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            var validCategories = new List<string> { "Vợt", "Bóng", "Cầu", "Đệm", "Quần áo" };

            // Kiểm tra danh mục
            if (string.IsNullOrEmpty(product.Category) || !validCategories.Contains(product.Category))
            {
                ModelState.AddModelError("Category", "Danh mục không hợp lệ. Chỉ chấp nhận: Vợt, Bóng, Cầu, Đệm, Quần áo.");
            }

            // Kiểm tra ngày sản xuất
            if (product.ManufacturingDate >= DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("ManufacturingDate", "Ngày sản xuất phải nhỏ hơn ngày hiện tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = validCategories;
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}