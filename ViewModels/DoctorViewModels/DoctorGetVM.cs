namespace ExamMVC.ViewModels.DoctorViewModels
{
    public class DoctorGetVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string SpecialityName { get; set; } = string.Empty;
    }
}
