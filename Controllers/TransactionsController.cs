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
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transaction.Include(t => t.Borrower).Include(t => t.Item).Include(t => t.Lender);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Borrower)
                .Include(t => t.Item)
                .Include(t => t.Lender)
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["BorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "Name");
            ViewData["LenderID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("TransactionID,LenderID,BorrowerID,ItemID,StartDate,ExpectedEndDate,ActualEndDate,RatingGiven,Comment")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", transaction.BorrowerID);
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "Name", transaction.ItemID);
            ViewData["LenderID"] = new SelectList(_context.Set<User>(), "UserID", "Email", transaction.LenderID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["BorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", transaction.BorrowerID);
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "Name", transaction.ItemID);
            ViewData["LenderID"] = new SelectList(_context.Set<User>(), "UserID", "Email", transaction.LenderID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionID,LenderID,BorrowerID,ItemID,StartDate,ExpectedEndDate,ActualEndDate,RatingGiven,Comment")] Transaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionID))
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
            ViewData["BorrowerID"] = new SelectList(_context.Set<User>(), "UserID", "Email", transaction.BorrowerID);
            ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "Name", transaction.ItemID);
            ViewData["LenderID"] = new SelectList(_context.Set<User>(), "UserID", "Email", transaction.LenderID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Borrower)
                .Include(t => t.Item)
                .Include(t => t.Lender)
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transaction == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transaction'  is null.");
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return (_context.Transaction?.Any(e => e.TransactionID == id)).GetValueOrDefault();
        }
    }
}
