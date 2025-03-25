using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointments.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public IActionResult Index()
        {
            var doctors = _doctorService.GetAll();
            return View(doctors);
        }
    }
}
