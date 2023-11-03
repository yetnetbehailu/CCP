using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Models.DogModels;

namespace CCP.Controllers
{
    public class ChampionshipTitleController : Controller
    {
        private readonly CCPContext _context;

        public ChampionshipTitleController(CCPContext context)
        {
            _context = context;
        }

        // GET: ChampionshipTitle
        public async Task<IActionResult> Index()
        {
            var cCPContext = _context.OfficialTitle;
            return View(await cCPContext.ToListAsync());
        }

        // GET: ChampionshipTitle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChampionshipTitle == null)
            {
                return NotFound();
            }

            var championshipTitle = await _context.ChampionshipTitle
                .Include(c => c.Dog)
                .Include(c => c.OfficialTitle)
                .Where(c => c.OfficialTitleID == id)
                .ToListAsync();
            if (championshipTitle == null)
            {
                return NotFound();
            }

            return View(championshipTitle);
        }
    }
}
