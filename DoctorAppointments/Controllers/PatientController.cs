using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointments.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        public IActionResult Index()
        {
            var patients = _patientService.GetAll();
            return View(patients);
        }

        public IActionResult Create()
        {
            CreatePatientFormViewModel viewModel = new();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePatientFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (success, errorMessage) = await _patientService.Store(model);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, errorMessage ?? "An unexpected error occurred.");
                return View(model);
            }

            TempData["Success"] = "Patient created successfully!";
            return RedirectToAction(nameof(Create));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patient = _patientService.GetById(id);

            if (patient is null)
                return NotFound();

            EditPatientFormViewModel viewModel = new()
            {
                Id = id,
                Name = patient.Name,
                Email = patient.Email,
                Phone = patient.Phone
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPatientFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (updatedPatient, errorMessage) = await _patientService.Update(model);

            if (updatedPatient is null)
            {
                ModelState.AddModelError("", errorMessage ?? "An unexpected error occurred.");
                return View(model);
            }

            TempData["Success"] = "Patient updated successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
