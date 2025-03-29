namespace DoctorAppointments.ViewModels.Patient
{
    public class PatientFormViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;
        [MaxLength(20)]
        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}
