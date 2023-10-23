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
                if (userExist.Breeder == null) //  If the user exists, then check if their Breeder navigation property is null. If it is null, that means they don't have a Breeder associated with them, enable to create one
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
    }
}
