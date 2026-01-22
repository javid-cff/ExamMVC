using System.Diagnostics;
using System.Threading.Tasks;
using ExamMVC.Contexts;
using ExamMVC.Models;
using ExamMVC.ViewModels.DoctorViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamMVC.Controllers
{
    public class HomeController(ILogger<HomeController> _logger, AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors.Select(x => new DoctorGetVM()
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                ImagePath = x.ImagePath,
                SpecialityName = x.Speciality.Name
            }).ToListAsync();

            return View(doctors);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
