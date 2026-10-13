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

        private readonly IClubRepository _cluRepository;

        public ClubController(IClubRepository clubRepository)
        {
            _cluRepository = clubRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _cluRepository.GetAllClubs();
            return View(clubs);
        }

        public async Task<IActionResult> ClubDetails(int id)
        {
            Club? club = await _cluRepository.GetClubByIdAsnc(id);
            return View(club);
        }

    }
}
