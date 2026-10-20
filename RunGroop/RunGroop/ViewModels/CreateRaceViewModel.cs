using RunGroop.Data.Enum;
using RunGroop.Models;

namespace RunGroop.ViewModels
{
    public class CreateRaceViewModel
    {

        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; }

    }
}
