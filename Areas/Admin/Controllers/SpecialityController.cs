using System.Threading.Tasks;
using ExamMVC.Contexts;
using ExamMVC.Models;
using ExamMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class SpecialityController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var specialities = await _context.Specialities.Select(x => new SpecialityVM()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return View(specialities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialityVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            Speciality speciality = new Speciality()
            {
                Id = vm.Id,
                Name = vm.Name
            };

            await _context.Specialities.AddAsync(speciality);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var speciality = await _context.Specialities.FindAsync(id);

            if (speciality == null)
                return BadRequest();

            _context.Remove(speciality);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var speciality = await _context.Specialities.FindAsync(id);

            if (speciality == null)
                return BadRequest();

            SpecialityVM vm = new SpecialityVM()
            {
                Id = speciality.Id,
                Name = speciality.Name
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpecialityVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var existSpeciality = await _context.Specialities.FindAsync(vm.Id);

            if (existSpeciality == null)
                return BadRequest();

            existSpeciality.Id = vm.Id;
            existSpeciality.Name = vm.Name;

            _context.Specialities.Update(existSpeciality);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
