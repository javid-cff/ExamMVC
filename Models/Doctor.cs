namespace ExamMVC.Models
{
    public class Doctor : BaseEntity
    {
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; } = null!;
    }
}
