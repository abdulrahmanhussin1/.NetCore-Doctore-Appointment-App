using DoctorAppointments.ViewModels.Specialization;

namespace DoctorAppointments.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }
        public IActionResult Index()
        {
            var specializations = _specializationService.GetAll();
            return View(specializations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateSpecializationFormViewModel viewModel = new();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpecializationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _specializationService.Store(model);
            TempData["Success"] = "Specialization created successfully!";
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var specialization = _specializationService.GetById(id);

            if (specialization is null)
                return NotFound();

            EditSpecializationFormViewModel viewModel = new()
            {
                Id = id,
                Name = specialization.Name,
                Description = specialization.Description,
                CurrentImg = specialization.Img
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSpecializationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var specialization = await _specializationService.Update(model);

            if (specialization is null)
                return BadRequest();
            TempData["Success"] = "Specialization Updated successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
