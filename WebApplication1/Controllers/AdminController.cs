using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private ISongRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        public AdminController(ApplicationDbContext context, ISongRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }
        public async Task<ViewResult> Index() => View(await _productRepository.SongsAsync());

        public async Task<ViewResult> OrderDetails() => View(await _productRepository.OrdersAsync());

        public async Task<IActionResult> Edit(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _productRepository.SearchAsync(productId.Value);

                
            if (product == null)
            {
                return NotFound();
            }
            await PopulateCategorysDropDownList(product.CategoryID);
            return View(product);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null) { return NotFound(); }
            var productToUpdate = await _productRepository.SearchAsync(id.Value);
            if (await TryUpdateModelAsync<SongModel>(productToUpdate,
                "",
                c => c.Name, c => c.Artist, c => c.Price, c => c.CategoryID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["message"] = $"{productToUpdate.Name} has been saved";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, " + "see your system administrator.");
                }
            }
            await PopulateCategorysDropDownList(productToUpdate.CategoryID);
            return View(productToUpdate);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategorysDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Artist,Price,CategoryID")] SongModel product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["message"] = $"{product.Name} has been created";
                return RedirectToAction("Index");
            }
            await PopulateCategorysDropDownList(product.CategoryID);
            return View(product);
        }


        public async Task<IActionResult> Delete(int? ProductID, bool? saveChangesError = false)
        {
            if (ProductID == null)
            {
                return NotFound();
            }
            var product = await _productRepository.SearchAsync(ProductID.Value);
            //var product = await _context.Products    
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync(m => m.ProductID == ProductID);
            if (product == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.SearchAsync(id);
            //var course = await _context.Products
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync(m => m.ProductID == ProductID);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Songs.Remove(product);
                await _context.SaveChangesAsync();
                TempData["message"] = $"{product.Name} has been deleted";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private async Task PopulateCategorysDropDownList(object selectedCategoryID = null)
        {
            //var departmentsQuery = from d in _context.Departments
            //                       orderby d.Name
            //                       select d;
            var categories = await _categoryRepository.CategoriesAsNoTrackingAsync();
            ViewBag.Categories = new SelectList(categories,"CategoryID", "Name", selectedCategoryID);
        }
    }
}
