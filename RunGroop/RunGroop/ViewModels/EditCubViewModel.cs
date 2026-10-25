using RunGroop.Data.Enum;
using RunGroop.Models;
using System.ComponentModel.DataAnnotations;

namespace RunGroop.ViewModels
{
    public class EditCubViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile? Image { get; set; }
        public ClubCategory Category { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? URL { get; set; }
    }
}
