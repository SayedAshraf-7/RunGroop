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
    public class ClubController : Controller
    {

        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepository , IPhotoService PhotoService)
        {
            _clubRepository = clubRepository;
            _photoService = PhotoService;
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


        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetClubByIdAsnc(id);
            var clubVM = new EditCubViewModel
            {
                Id = club.Id,
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                Category = club.ClubCategory
            };
            return View(clubVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View(clubVM);
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
            if (userClub == null) return NotFound();

            string imageUrl = userClub.Image;   // keep the old image by default

            // Only touch the photo if the user picked a new file
            if (clubVM.Image != null)
            {
                var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Photo upload failed");
                    return View(clubVM);
                }

                if (!string.IsNullOrEmpty(userClub.Image))
                {
                    try { await _photoService.DeletePhotoAsync(userClub.Image); }
                    catch { /* old photo cleanup failed, don't block the edit */ }
                }

                imageUrl = photoResult.Url.ToString();
            }

            var club = new Club
            {
                Id = id,                       // use the route id, not clubVM.Id
                Title = clubVM.Title,
                Description = clubVM.Description,
                ClubCategory = clubVM.Category,
                Image = imageUrl,
                AddressId = clubVM.AddressId,
                Address = clubVM.Address
            };

            _clubRepository.UpdateClub(club);
            return RedirectToAction("Index");
        }

    }
}
