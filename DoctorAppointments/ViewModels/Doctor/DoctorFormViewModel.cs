namespace DoctorAppointments.ViewModels.Doctora
{
    public class DoctorFormViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Specialization")]
        public int SpecializationId { get; set; }

        public IEnumerable<SelectListItem> Specializations { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
