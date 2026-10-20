using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroop.Data;
using RunGroop.Interfaces;
using RunGroop.Models;
using RunGroop.Repository;
using RunGroop.ViewModels;
using RunGroopWebApp.Interfaces;

namespace RunGroop.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        public RaceController(IRaceRepository raceRepository , IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateRaceViewModel RaceVM)
        {
            if (!ModelState.IsValid)
                return View(RaceVM);

            var result = await _photoService.AddPhotoAsync(RaceVM.Image);

            if (result.Error != null)
            {
                ModelState.AddModelError(nameof(RaceVM.Image), "Photo upload failed. Please try again.");
                return View(RaceVM);
            }

            var club = new Race
            {
                Title = RaceVM.Title,
                Description = RaceVM.Description,
                Image = result.Url.ToString(),
                Address = new Address
                {
                    Street = RaceVM.Address.Street,
                    City = RaceVM.Address.City,
                    State = RaceVM.Address.State
                }
            };
            _raceRepository.AddRace(club);
            return RedirectToAction("Index");
        }

    }
}
