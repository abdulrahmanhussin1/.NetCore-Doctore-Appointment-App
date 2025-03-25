namespace DoctorAppointments.Services.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
