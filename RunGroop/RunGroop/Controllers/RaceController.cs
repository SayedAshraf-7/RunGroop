using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroop.Data;
using RunGroop.Models;

namespace RunGroop.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Races = _context.Races.ToList();
            return View(Races);
        }

        public IActionResult RaceDetails(int id)
        {
            Race? Race = _context.Races.Include(Race => Race.Address)
                .FirstOrDefault(c => c.Id == id);
            return Race == null ? NotFound() : View(Race);
        }

    }
}
