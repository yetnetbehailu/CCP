using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Models.BreederModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CCP.Controllers
{
    public class BreederController : Controller
    {
        private readonly CCPContext _context;

        public BreederController(CCPContext context)
        {
            _context = context;
        }

        // GET: Breeder
        public async Task<IActionResult> Index()
        {
            var cCPContext = _context.Breeder.Include(b => b.Country).Include(b => b.User);
            return View(await cCPContext.ToListAsync());
        }

        // GET: Breeder/Create
        public IActionResult Create()
        {
            var countries = _context.Country.ToList();
            ViewData["CountryList"] = new SelectList(countries, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Ensure the user is authenticated
        public async Task<IActionResult> Create(Breeder breeder)
        {
            var countries = _context.Country.ToList();
            ViewData["CountryList"] = new SelectList(countries, "ID", "Name");

            // Get the currently logged-in user's ID
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the user in the database
            var userExist = _context.Users.Include(b => b.Breeder).FirstOrDefault(u => u.Id == currentUserId);

            if (userExist != null)
            {
                if (userExist.Breeder == null) // If the user exists, then check if their Breeder navigation property is null. If it is null, that means they don't have a Breeder associated with them, enable to create one
                {
                    // Associate the logged-in user with the new Breeder
                    breeder.UserID = currentUserId;

                    _context.Breeder.Add(breeder);
                    _context.SaveChanges();
                }

                // Add validation return the view with validation errors
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Breeder/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breeder = await _context.Breeder.FindAsync(id);
            if (breeder == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (breeder.UserID != currentUserId)
            {
                return Forbid(); // Return a forbidden response
            }

            var countries = _context.Country.ToList();
            ViewData["CountryList"] = new SelectList(countries, "ID", "Name", breeder.CountryID);
            return View(breeder);
        }

        // POST: Breeder/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Breeder breeder) // Breeder UserId is null here
        {
            if (id == breeder.ID)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // Set the breeder UserID to the current user's ID since userId wasn't captured during the edit form post
                breeder.UserID = currentUserId;
                _context.Update(breeder); // Updates the breeder object
                await _context.SaveChangesAsync();

                var countries = _context.Country.ToList();
                ViewData["CountryList"] = new SelectList(countries, "ID", "Name", breeder.CountryID);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
