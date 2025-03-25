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
    }
}
