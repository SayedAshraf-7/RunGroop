using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroop.Data;
using RunGroop.Interfaces;
using RunGroop.Models;
using RunGroop.Repository;

namespace RunGroop.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            var Races = await _raceRepository.GetAllRaces();
            return View(Races);
        }

        public async Task<IActionResult> RaceDetails(int id)
        {
            Race? Race = await _raceRepository.GetRaceByIdAsnc(id);
            return View(Race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
                return View(race);
            _raceRepository.AddRace(race);
            return RedirectToAction("Index");
        }

    }
}
