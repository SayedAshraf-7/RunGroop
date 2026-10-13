using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroop.Data;
using RunGroop.Interfaces;
using RunGroop.Models;

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

    }
}
