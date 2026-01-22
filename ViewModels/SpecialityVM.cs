using System.ComponentModel.DataAnnotations;

namespace ExamMVC.ViewModels
{
    public class SpecialityVM
    {
        public int Id { get; set; }
        [Required, MaxLength(70), MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
