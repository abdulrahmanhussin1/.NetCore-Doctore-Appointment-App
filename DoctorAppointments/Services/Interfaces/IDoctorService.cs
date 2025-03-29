namespace DoctorAppointments.Services.Interfaces
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
        Doctor? GetById(int id);
        IEnumerable<SelectListItem> GetSelectList();
        Task<(bool Success, string? ErrorMessage)> Store(CreateDoctorFormViewModel model);
        Task<(Doctor? UpdatedDoctor, string? ErrorMessage)> Update(EditDoctorFormViewModel model);
    }
}
