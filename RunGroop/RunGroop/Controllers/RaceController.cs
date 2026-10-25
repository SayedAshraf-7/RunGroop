using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroop.Interfaces;
using RunGroop.Models;
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
        
        
        
        public async Task<IActionResult> Edit(int id)
        {
            var Race = await _raceRepository.GetRaceByIdAsnc(id);
            var clubVM = new EditRaceViewModel
            {
                Id = Race.Id,
                Title = Race.Title,
                Description = Race.Description,
                AddressId = Race.AddressId,
                Address = Race.Address,
                URL = Race.Image,
                RaceCategory = Race.RaceCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel RaceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View(RaceVM);
            }

            var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);
            if (userRace == null) return NotFound();

            string imageUrl = userRace.Image;   // keep the old image by default

            // Only touch the photo if the user picked a new file
            if (RaceVM.Image != null)
            {
                var photoResult = await _photoService.AddPhotoAsync(RaceVM.Image);
                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Photo upload failed");
                    return View(RaceVM);
                }

                if (!string.IsNullOrEmpty(userRace.Image))
                {
                    try { await _photoService.DeletePhotoAsync(userRace.Image); }
                    catch { /* old photo cleanup failed, don't block the edit */ }
                }

                imageUrl = photoResult.Url.ToString();
            }

            var race = new Race
            {
                Id = id,                       // use the route id, not clubVM.Id
                Title = RaceVM.Title,
                Description = RaceVM.Description,
                RaceCategory = RaceVM.RaceCategory,
                Image = imageUrl,
                AddressId = RaceVM.AddressId,
                Address = RaceVM.Address
            };

            _raceRepository.UpdateClub(race);
            return RedirectToAction("Index");
        }


    }
}
