using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroop.Data;
using RunGroop.Interfaces;
using RunGroop.Models;
using RunGroop.Repository;

namespace RunGroop.Controllers
{
    public class ClubController : Controller
    {

        private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAllClubs();
            return View(clubs);
        }

        public async Task<IActionResult> ClubDetails(int id)
        {
            Club? club = await _clubRepository.GetClubByIdAsnc(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if (!ModelState.IsValid)
                return View(club);
            _clubRepository.AddClub(club);
            return RedirectToAction("Index");
        }



    }
}
