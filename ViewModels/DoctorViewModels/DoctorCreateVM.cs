using System.ComponentModel.DataAnnotations;

namespace ExamMVC.ViewModels.DoctorViewModels
{
    public class DoctorCreateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(70), MinLength(3)]
        public string Fullname { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required]
        public int SpecialityId { get; set; }
    }
}
