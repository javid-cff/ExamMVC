namespace ExamMVC.Models
{
    public class Speciality : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
