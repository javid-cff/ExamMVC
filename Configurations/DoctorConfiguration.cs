using ExamMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamMVC.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Email).IsRequired(false);
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasOne(x => x.Speciality).WithMany(x => x.Doctors).HasForeignKey(x => x.SpecialityId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
