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
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Item.Include(i => i.Category).Include(i => i.CurrentBorrower).Include(i => i.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Category)
                .Include(i => i.CurrentBorrower)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<ItemCategory>(), "CategoryID", "Name");
            ViewData["CurrentBorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            ViewData["OwnerID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ItemID,Name,Description,CategoryID,OwnerID,CurrentBorrowerID,BorrowPeriod,SecurityDeposit,Image,Status")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<ItemCategory>(), "CategoryID", "Name", item.CategoryID);
            ViewData["CurrentBorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", item.CurrentBorrowerID);
            ViewData["OwnerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", item.OwnerID);
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<ItemCategory>(), "CategoryID", "Name", item.CategoryID);
            ViewData["CurrentBorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", item.CurrentBorrowerID);
            ViewData["OwnerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", item.OwnerID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ItemID,Name,Description,CategoryID,OwnerID,CurrentBorrowerID,BorrowPeriod,SecurityDeposit,Image,Status")] Item item)
        {
            if (id != item.ItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemID))
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
            ViewData["CategoryID"] = new SelectList(_context.Set<ItemCategory>(), "CategoryID", "Name", item.CategoryID);
            ViewData["CurrentBorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", item.CurrentBorrowerID);
            ViewData["OwnerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", item.OwnerID);
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Category)
                .Include(i => i.CurrentBorrower)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Item == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Item'  is null.");
            }
            var item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                _context.Item.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Item?.Any(e => e.ItemID == id)).GetValueOrDefault();
        }
    }
}
