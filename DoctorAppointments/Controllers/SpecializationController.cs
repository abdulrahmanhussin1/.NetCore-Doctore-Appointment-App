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
    }
}
