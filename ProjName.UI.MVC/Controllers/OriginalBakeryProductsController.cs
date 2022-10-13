using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjName.DATA.EF.Models;

namespace ProjName.UI.MVC.Controllers
{
    public class OriginalBakeryProductsController : Controller
    {
        private readonly StoreFrontContext _context;

        public OriginalBakeryProductsController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: BakeryProducts
        public async Task<IActionResult> Index()
        {
            var storefrontContext = _context.BakeryProducts.Include(b => b.Category).Include(b => b.Seaonal);
            return View(await storefrontContext.ToListAsync());
        }

        // GET: BakeryProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BakeryProducts == null)
            {
                return NotFound();
            }

            var bakeryProduct = await _context.BakeryProducts
                .Include(b => b.Category)
                .Include(b => b.Seaonal)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (bakeryProduct == null)
            {
                return NotFound();
            }

            return View(bakeryProduct);
        }

        // GET: BakeryProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SeaonalId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonName");
            return View();
        }

        // POST: BakeryProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,BakeryItems,QuantityPerUnit,Price,Discontinued,SeaonalId")] BakeryProduct bakeryProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bakeryProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", bakeryProduct.CategoryId);
            ViewData["SeaonalId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonName", bakeryProduct.SeaonalId);
            return View(bakeryProduct);
        }

        // GET: BakeryProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BakeryProducts == null)
            {
                return NotFound();
            }

            var bakeryProduct = await _context.BakeryProducts.FindAsync(id);
            if (bakeryProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", bakeryProduct.CategoryId);
            ViewData["SeaonalId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonName", bakeryProduct.SeaonalId);
            return View(bakeryProduct);
        }

        // POST: BakeryProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,BakeryItems,QuantityPerUnit,Price,Discontinued,SeaonalId")] BakeryProduct bakeryProduct)
        {
            if (id != bakeryProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bakeryProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BakeryProductExists(bakeryProduct.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", bakeryProduct.CategoryId);
            ViewData["SeaonalId"] = new SelectList(_context.Seasons, "SeasonId", "SeasonName", bakeryProduct.SeaonalId);
            return View(bakeryProduct);
        }

        // GET: BakeryProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BakeryProducts == null)
            {
                return NotFound();
            }

            var bakeryProduct = await _context.BakeryProducts
                .Include(b => b.Category)
                .Include(b => b.Seaonal)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (bakeryProduct == null)
            {
                return NotFound();
            }

            return View(bakeryProduct);
        }

        // POST: BakeryProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BakeryProducts == null)
            {
                return Problem("Entity set 'StorefrontContext.BakeryProducts'  is null.");
            }
            var bakeryProduct = await _context.BakeryProducts.FindAsync(id);
            if (bakeryProduct != null)
            {
                _context.BakeryProducts.Remove(bakeryProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BakeryProductExists(int id)
        {
          return _context.BakeryProducts.Any(e => e.ProductId == id);
        }
    }
}
