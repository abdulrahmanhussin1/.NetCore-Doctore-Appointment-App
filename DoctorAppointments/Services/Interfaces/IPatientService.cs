namespace DoctorAppointments.Services.Interfaces
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetAll();

        IEnumerable<SelectListItem> GetSelectList();
    }
}
