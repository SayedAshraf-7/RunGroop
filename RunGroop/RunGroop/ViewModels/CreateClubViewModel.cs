using RunGroop.Data.Enum;
using RunGroop.Models;
using System.ComponentModel.DataAnnotations;

namespace RunGroop.ViewModels
{
    public class CreateClubViewModel
    {
        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }


    }
}
