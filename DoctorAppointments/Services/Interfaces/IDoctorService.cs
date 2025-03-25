namespace DoctorAppointments.Services.Interfaces
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
        IEnumerable<SelectListItem> GetSelectList();
    }
}
