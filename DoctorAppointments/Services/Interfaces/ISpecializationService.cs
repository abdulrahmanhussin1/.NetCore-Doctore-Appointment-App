namespace DoctorAppointments.Services.Interfaces
{
    public interface ISpecializationService
    {
        IEnumerable<Specialization> GetAll();
        IEnumerable<SelectListItem> GetSelectList();
    }
}
