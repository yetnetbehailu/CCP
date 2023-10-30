using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Models.DogModels;
using CCP.ViewModels;
using CCP.Helpers;

namespace CCP.Controllers
{
    public class DogController : Controller
    {
        private readonly CCPContext _context;

        public DogController(CCPContext context)
        {
            _context = context;
        }

        // GET: Dog
        public async Task<IActionResult> Index()
        {
            var dog = _context.Dog.Include(d => d.Breeder).Include(d => d.Kennel).Include(d => d.Owner).ToList();
            List<DogViewModel> listModel = new List<DogViewModel>();
            foreach(var d in dog)
            {
                listModel.Add(
                    new DogViewModel
                    {
                        Dog = d,
                        Breeder = _context.Breeder.FirstOrDefault(b => b.UserID == d.BreederID),
                        Kennel = _context.Kennel.FirstOrDefault(k => k.UserId == d.KennelID)
                    }
                );
            }
            return View(listModel);
        }

        // GET: Dog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dog == null)
            {
                return NotFound();
            }

            var dog = await _context.Dog
                .Include(d => d.Breeder).ThenInclude(u => u.Breeder)
                .Include(d => d.Kennel).ThenInclude(u => u.Kennel)
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dog == null)
            {
                return NotFound();
            }
            var model = new DogViewModel
            {
                Dog = dog,
                Breeder = _context.Breeder.FirstOrDefault(b => b.UserID == dog.BreederID),
                Kennel = _context.Kennel.FirstOrDefault(k => k.UserId == dog.KennelID)
            };
            return View(model);
        }

        // GET: Dog/Create
        public IActionResult Create()
        {
            //  owner is the user itself
            //  breeder show all to choose from
            //  defaults to null
            //  same for kennel
            var loggedUser = _context.User.Include(u => u.Breeder).Include(u => u.Kennel).FirstOrDefault(u => u.Id == "user2");
            var breeder = _context.Breeder.Where(b => b.UserID != null);
            var kennel = _context.Kennel.Where(k => k.UserId != null);

            ViewData["BreederID"] = new SelectList(breeder, "UserID", "UserID");
            ViewData["KennelID"] = new SelectList(kennel, "UserId", "UserId");
            ViewData["OwnerID"] = loggedUser;
            ViewData["Gender"] = EnumHelper.ConvertEnumToRadioList<Genders>();
            ViewData["Coat"] = EnumHelper.ConvertEnumToRadioList<Coats>();
            return View();
        }

        // POST: Dog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RegName,RegNo,PetName,DOB,YearOfDeath,Coat,Gender,Color,Height,Weight,OwnerID,BreederID,KennelID")] Dog dog)
        {
            // dog.OwnerID = "user2";
            // dog.BreederID = "user2";
            _context.Dog.Add(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Dog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dog == null)
            {
                return NotFound();
            }

            var dog = await _context.Dog.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }
            var breeder = _context.Breeder.Where(b => b.UserID != null);
            var kennel = _context.Kennel.Where(k => k.UserId != null);
            ViewData["BreederID"] = new SelectList(breeder, "UserID", "UserID", dog.BreederID);
            ViewData["KennelID"] = new SelectList(kennel, "UserId", "UserId", dog.KennelID);
            ViewData["OwnerID"] = new SelectList(_context.User, "Id", "Id", dog.OwnerID);
            ViewData["Gender"] = EnumHelper.ConvertEnumToRadioList<Genders>();
            ViewData["Coat"] = EnumHelper.ConvertEnumToRadioList<Coats>();
            return View(dog);
        }

        // POST: Dog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RegName,RegNo,PetName,DOB,YearOfDeath,Coat,Gender,Color,Height,Weight,OwnerID,BreederID,KennelID")] Dog dog)
        {
            if (id != dog.ID)
            {
                return NotFound();
            }

            _context.Update(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Dog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dog == null)
            {
                return NotFound();
            }

            var dog = await _context.Dog
                .Include(d => d.Breeder)
                .Include(d => d.Kennel)
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dog == null)
            {
                return NotFound();
            }
            var model = new DogViewModel
            {
                Dog = dog,
                Breeder = _context.Breeder.FirstOrDefault(b => b.UserID == dog.BreederID),
                Kennel = _context.Kennel.FirstOrDefault(k => k.UserId == dog.KennelID)
            };
            return View(model);
        }

        // POST: Dog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dog == null)
            {
                return Problem("Entity set 'CCPContext.Dog'  is null.");
            }
            var dog = await _context.Dog.FindAsync(id);
            if (dog != null)
            {
                _context.Dog.Remove(dog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DogExists(int id)
        {
            return (_context.Dog?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
