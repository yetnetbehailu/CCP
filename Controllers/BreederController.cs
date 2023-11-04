using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Models.BreederModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;
using CCP.Models;
using CCP.Areas.Identity.Data;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CCP.ViewModels;

namespace CCP.Controllers
{
    public class BreederController : Controller
    {
        private readonly CCPContext _context;

        public BreederController(CCPContext context)
        {
            _context = context;
        }

        // Get the currently logged-in user's ID
        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private bool UserIsBreeder => _context.Users.Include(b => b.Breeder).Any(u => u.Id == CurrentUserId && u.Breeder != null);

        // GET: Breeder
        public async Task<IActionResult> Index()
        {
            var cCPContext = _context.Breeder.Include(b => b.Country).Include(b => b.User);

            var userBreeder = _context.Breeder.FirstOrDefault(b => b.UserID == CurrentUserId);
            int? userBreederId = userBreeder?.ID;

            ViewData["UserIsBreeder"] = UserIsBreeder;
            ViewData["UserBreederId"] = userBreederId;

            return View(await cCPContext.ToListAsync());
        }

        // GET: Breeder/Create
        [Authorize]
        public IActionResult Create()
        {
            var userExist = _context.Users.Include(b => b.Breeder).FirstOrDefault(u => u.Id == CurrentUserId);
            if (userExist != null && userExist.Breeder != null)
            {
                return RedirectToAction("Details", new { id = userExist.Breeder.ID });
            }

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

            // Find the user in the database
            var userExist = _context.Users.Include(b => b.Breeder).FirstOrDefault(u => u.Id == CurrentUserId);

            if (userExist != null)
            {
                if (userExist.Breeder == null)
                {
                    // Associate the logged-in user with the new Breeder
                    breeder.UserID = CurrentUserId;

                    _context.Breeder.Add(breeder);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(breeder); ;
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

            if (breeder.UserID != CurrentUserId)
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
        [Authorize]
        public async Task<IActionResult> Edit(int id, Breeder breeder) // Breeder UserId is null here
         {
            if (id == breeder.ID)
            {
                CCPUser user = await _context.User.FirstOrDefaultAsync(u => u.Id == CurrentUserId);
                //Get breeder before changes
                Breeder originalBreederBeforeChanges = await _context.Breeder.AsNoTracking().FirstOrDefaultAsync(b => b.ID == id);
                //Compare the two versions
                bool hasChanged = breeder.Name != originalBreederBeforeChanges.Name ||
                    breeder.CountryID != originalBreederBeforeChanges.CountryID ||
                    breeder.Address != originalBreederBeforeChanges.Address ||
                    breeder.Phone != originalBreederBeforeChanges.Phone;
                if (hasChanged)
                {
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    };
                    ChangeLog changeLog = new ChangeLog
                    {
                        User = user,
                        ChangeTime = DateTime.UtcNow,
                        ModelName = breeder.Name,
                        ChangeType = "Edited breeder",
                        OldValues = JsonConvert.SerializeObject(originalBreederBeforeChanges, settings),
                        NewValues = JsonConvert.SerializeObject(breeder, settings)
                    };
                    _context.ChangeLogs.Add(changeLog);
                }
                // Set the breeder UserID to the current user's ID since userId wasn't captured during the edit form post
                breeder.UserID = CurrentUserId;
                _context.Update(breeder);
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

        // Get: Breeder/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breeder = await _context.Breeder.Include(b => b.Country).Include(b => b.User)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (breeder == null)
            {
                return NotFound();
            }

            if (breeder.UserID != CurrentUserId)
            {
                return Forbid();
            }

            return View(breeder);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var breeder = await _context.Breeder.FindAsync(id);
            if (breeder == null)
            {
                return NotFound();
            }

            _context.Breeder.Remove(breeder);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Breeder/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breeder = await _context.Breeder.Include(b => b.User).Include(b => b.Country)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (breeder == null)
            {
                return NotFound();
            }

            var dogs = await _context.Dog.Where(d => d.BreederID == breeder.UserID).ToListAsync();

            var viewModel = new BreederDetailsViewModel
            {
                Breeder = breeder,
                Dogs = dogs
            };

            return View(viewModel);
        }

    }
}
