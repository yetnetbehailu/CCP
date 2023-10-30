using CCP.Data;
using CCP.Models;
using CCP.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CCP.Controllers
{
    public class ChangeLogsController : Controller
    {
        private readonly CCPContext _context;
        public ChangeLogsController(CCPContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var logs = _context.ChangeLogs.ToList();
            return View(logs);
        }
        public async Task<IActionResult> Details(int id)
        {
            if(id == 0 || _context.ChangeLogs == null)
            {
                return NotFound();
            }
            ChangeLog? changeLog = await _context.ChangeLogs.Include(c => c.User).FirstOrDefaultAsync(changeLog => changeLog.Id == id);
            if(changeLog == null)
            {
                return NotFound();
            }
            string? oldVersion = changeLog.OldValues;
            string newVersion = changeLog.NewValues;
            UtilityClass uti = new UtilityClass();
            List<string> diff = uti.GetDifferences(oldVersion, newVersion);
            ViewData["Changes"] = diff;
            return View(changeLog);
        }
    }
}
