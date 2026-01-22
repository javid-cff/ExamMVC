using System.ComponentModel.DataAnnotations;

namespace ExamMVC.ViewModels.AppUserViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(255), MinLength(3)]
        public string Fullname { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(3)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(8), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
        public bool IsRemember { get; set; }
    }
}
