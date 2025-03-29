namespace DoctorAppointments.Services.Interfaces
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetAll();
        Patient? GetById(int id);
        IEnumerable<SelectListItem> GetSelectList();
        Task<(bool Success, string? ErrorMessage)> Store(CreatePatientFormViewModel model);
        Task<(Patient? UpdatedPatient, string? ErrorMessage)> Update(EditPatientFormViewModel model);
    }
}
