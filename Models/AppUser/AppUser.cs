using Microsoft.AspNetCore.Identity;

namespace ExamMVC.Models.AppUser
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
