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
    [Authorize]
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ratings
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rating.Include(r => r.Ratee).Include(r => r.Rater).Include(r => r.Transaction);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Ratee)
                .Include(r => r.Rater)
                .Include(r => r.Transaction)
                .FirstOrDefaultAsync(m => m.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["RateeID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            ViewData["RaterID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            ViewData["TransactionID"] = new SelectList(_context.Set<Transaction>(), "TransactionID", "TransactionID");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("RatingID,RaterID,RateeID,TransactionID,Score,Comment")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RateeID"] = new SelectList(_context.Set<User>(), "UserID", "Email", rating.RateeID);
            ViewData["RaterID"] = new SelectList(_context.Set<User>(), "UserID", "Email", rating.RaterID);
            ViewData["TransactionID"] = new SelectList(_context.Set<Transaction>(), "TransactionID", "TransactionID", rating.TransactionID);
            return View(rating);
        }

        // GET: Ratings/Edit/5       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["RateeID"] = new SelectList(_context.Set<User>(), "UserID", "Email", rating.RateeID);
            ViewData["RaterID"] = new SelectList(_context.Set<User>(), "UserID", "Email", rating.RaterID);
            ViewData["TransactionID"] = new SelectList(_context.Set<Transaction>(), "TransactionID", "TransactionID", rating.TransactionID);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("RatingID,RaterID,RateeID,TransactionID,Score,Comment")] Rating rating)
        {
            if (id != rating.RatingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingID))
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
            ViewData["RateeID"] = new SelectList(_context.Set<User>(), "UserID", "Email", rating.RateeID);
            ViewData["RaterID"] = new SelectList(_context.Set<User>(), "UserID", "Email", rating.RaterID);
            ViewData["TransactionID"] = new SelectList(_context.Set<Transaction>(), "TransactionID", "TransactionID", rating.TransactionID);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Ratee)
                .Include(r => r.Rater)
                .Include(r => r.Transaction)
                .FirstOrDefaultAsync(m => m.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rating == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rating'  is null.");
            }
            var rating = await _context.Rating.FindAsync(id);
            if (rating != null)
            {
                _context.Rating.Remove(rating);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
          return (_context.Rating?.Any(e => e.RatingID == id)).GetValueOrDefault();
        }
    }
}
