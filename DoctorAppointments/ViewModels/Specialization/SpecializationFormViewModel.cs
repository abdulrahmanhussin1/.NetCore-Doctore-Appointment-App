namespace DoctorAppointments.ViewModels.Specialization
{
    public class SpecializationFormViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
