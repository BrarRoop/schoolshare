using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolShare.Models;
using schoolshare.Data;
using Microsoft.AspNetCore.Authorization;

namespace schoolshare.Controllers
{
    public class ItemCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemCategories
        public async Task<IActionResult> Index()
        {
              return _context.ItemCategory != null ? 
                          View(await _context.ItemCategory.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ItemCategory'  is null.");
        }

        // GET: ItemCategories/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ItemCategory == null)
            {
                return NotFound();
            }

            var itemCategory = await _context.ItemCategory
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            return View(itemCategory);
        }

        // GET: ItemCategories/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("CategoryID,Name")] ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemCategory);
        }

        // GET: ItemCategories/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemCategory == null)
            {
                return NotFound();
            }

            var itemCategory = await _context.ItemCategory.FindAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return View(itemCategory);
        }

        // POST: ItemCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,Name")] ItemCategory itemCategory)
        {
            if (id != itemCategory.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCategoryExists(itemCategory.CategoryID))
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
            return View(itemCategory);
        }

        // GET: ItemCategories/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ItemCategory == null)
            {
                return NotFound();
            }

            var itemCategory = await _context.ItemCategory
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            return View(itemCategory);
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ItemCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ItemCategory'  is null.");
            }
            var itemCategory = await _context.ItemCategory.FindAsync(id);
            if (itemCategory != null)
            {
                _context.ItemCategory.Remove(itemCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCategoryExists(int id)
        {
          return (_context.ItemCategory?.Any(e => e.CategoryID == id)).GetValueOrDefault();
        }
    }
}
