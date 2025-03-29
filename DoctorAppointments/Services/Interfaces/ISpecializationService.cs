namespace DoctorAppointments.Services.Interfaces
{
    public interface ISpecializationService
    {
        IEnumerable<Specialization> GetAll();
        Specialization? GetById(int id);
        IEnumerable<SelectListItem> GetSelectList();

        Task Store(CreateSpecializationFormViewModel model);
        Task<Specialization?> Update(EditSpecializationFormViewModel model);
    }
}
