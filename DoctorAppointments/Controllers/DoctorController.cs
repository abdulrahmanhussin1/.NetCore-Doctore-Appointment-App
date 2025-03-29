using DoctorAppointments.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointments.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ISpecializationService _specializationService;
        public DoctorController(
            IDoctorService doctorService, ISpecializationService specializationService)
        {
            _doctorService = doctorService;
            _specializationService = specializationService;
        }
        public IActionResult Index()
        {
            var doctors = _doctorService.GetAll();
            return View(doctors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateDoctorFormViewModel viewModel = new();
            viewModel.Specializations = _specializationService.GetSelectList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Specializations = _doctorService.GetSelectList();
                return View(model);
            }
            var (success, errorMessage) = await _doctorService.Store(model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, errorMessage ?? "An unexpected error occurred.");
                model.Specializations = _doctorService.GetSelectList();

                return View(model);
            }
            TempData["Success"] = "Doctor created successfully!";
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var doctor = _doctorService.GetById(id);

            if (doctor is null)
                return NotFound();

            EditDoctorFormViewModel viewModel = new()
            {
                Id = id,
                Name = doctor.Name,
                SpecializationId = doctor.SpecializationId,
                Specializations = _specializationService.GetSelectList(),
                CurrentImg = doctor.Img
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDoctorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Specializations = _specializationService.GetSelectList();
                return View(model);
            }
            var (updatedDoctor, errorMessage) = await _doctorService.Update(model);

            if (updatedDoctor is null)
            {
                ModelState.AddModelError("", errorMessage ?? "An unexpected error occurred.");
                return View(model);
            }
            TempData["Success"] = "Doctor updated successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
