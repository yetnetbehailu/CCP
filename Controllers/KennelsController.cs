using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Models.KennelModels;

namespace CCP.Controllers
{
    public class KennelsController : Controller
    {
        private readonly CCPContext _context;

        public KennelsController(CCPContext context)
        {
            _context = context;
        }

        // GET: Kennels
        public async Task<IActionResult> Index()
        {
            var cCPContext = _context.Kennel.Include(k => k.Country);
            return View(await cCPContext.ToListAsync());
        }

        // GET: Kennels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kennel == null)
            {
                return NotFound();
            }

            var kennel = await _context.Kennel
                .Include(k => k.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kennel == null)
            {
                return NotFound();
            }

            return View(kennel);
        }

        // GET: Kennels/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "Name");
            return View();
        }

        // POST: Kennels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CountryID,Name,OwnerName,WebsiteURL,Address,Phone,Mobile,About")] Kennel kennel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kennel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "ID", kennel.CountryID);
            return View(kennel);
        }

        // GET: Kennels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kennel == null)
            {
                return NotFound();
            }

            var kennel = await _context.Kennel.FindAsync(id);
            if (kennel == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "ID", kennel.CountryID);
            return View(kennel);
        }

        // POST: Kennels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CountryID,Name,OwnerName,WebsiteURL,Address,Phone,Mobile,About")] Kennel kennel)
        {
            if (id != kennel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kennel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KennelExists(kennel.ID))
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
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "ID", kennel.CountryID);
            return View(kennel);
        }

        // GET: Kennels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kennel == null)
            {
                return NotFound();
            }

            var kennel = await _context.Kennel
                .Include(k => k.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kennel == null)
            {
                return NotFound();
            }

            return View(kennel);
        }

        // POST: Kennels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kennel == null)
            {
                return Problem("Entity set 'CCPContext.Kennel'  is null.");
            }
            var kennel = await _context.Kennel.FindAsync(id);
            if (kennel != null)
            {
                _context.Kennel.Remove(kennel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KennelExists(int id)
        {
          return (_context.Kennel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
