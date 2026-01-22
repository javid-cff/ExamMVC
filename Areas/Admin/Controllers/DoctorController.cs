using System.Numerics;
using System.Threading.Tasks;
using ExamMVC.Contexts;
using ExamMVC.Helpers;
using ExamMVC.Models;
using ExamMVC.ViewModels;
using ExamMVC.ViewModels.DoctorViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ExamMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _folderPath;

        public DoctorController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        }

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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await _sendSpecialitiesWithViewBag();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateVM vm)
        {
            await _sendSpecialitiesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            var isExistSpeciality = await _context.Specialities.AnyAsync(x => x.Id == vm.SpecialityId);

            if (!isExistSpeciality)
            {
                ModelState.AddModelError("SpecialityId", "This Speciality is not found!");
                return View(vm);
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "Image size must be maximum 2MB!");
                return View(vm);
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "Image format is not valid!");
                return View(vm);
            }

            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);

            Doctor doctor = new Doctor()
            {
                Id = vm.Id,
                Fullname = vm.Fullname,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber,
                ImagePath = uniqueFileName,
                SpecialityId = vm.SpecialityId
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return BadRequest();

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            string deletedImagePath = Path.Combine(_folderPath, doctor.ImagePath);

            FileHelper.FileDelete(deletedImagePath);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return BadRequest();

            DoctorUpdateVM vm = new DoctorUpdateVM()
            {
                Id = doctor.Id,
                Fullname = doctor.Fullname,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                SpecialityId = doctor.SpecialityId
            };

            await _sendSpecialitiesWithViewBag();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DoctorUpdateVM vm)
        {
            await _sendSpecialitiesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            var isExistSpeciality = await _context.Specialities.AnyAsync(x => x.Id == vm.SpecialityId);

            if (!isExistSpeciality)
            {
                ModelState.AddModelError("SpecialityId", "This Speciality is not found!");
                return View(vm);
            }

            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("Image", "Image size must be maximum 2MB!");
                return View(vm);
            }

            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("Image", "Image format is not valid!");
                return View(vm);
            }

            var existDoctor = await _context.Doctors.FindAsync(vm.Id);

            if (existDoctor == null)
                return BadRequest();

            existDoctor.Id = vm.Id;
            existDoctor.Fullname = vm.Fullname;
            existDoctor.Email = vm.Email;
            existDoctor.PhoneNumber = vm.PhoneNumber;
            existDoctor.SpecialityId = vm.SpecialityId;

            if (vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);

                string deletedImagePath = Path.Combine(_folderPath, existDoctor.ImagePath);

                FileHelper.FileDelete(deletedImagePath);

                existDoctor.ImagePath = newImagePath;
            }

            _context.Doctors.Update(existDoctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task _sendSpecialitiesWithViewBag()
        {
            var specialities = await _context.Specialities.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();

            ViewBag.Specialities = specialities;
        }
    }
}
