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


        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
                return View(clubVM);

            var result = await _photoService.AddPhotoAsync(clubVM.Image);

            if (result.Error != null)
            {
                ModelState.AddModelError(nameof(clubVM.Image), "Photo upload failed. Please try again.");
                return View(clubVM);
            }

            var club = new Club
            {
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = result.Url.ToString(),
                Address = new Address
                {
                    Street = clubVM.Address.Street,
                    City = clubVM.Address.City,
                    State = clubVM.Address.State
                }
            };
            _clubRepository.AddClub(club);
            return RedirectToAction("Index");
        }

        



    }
}
