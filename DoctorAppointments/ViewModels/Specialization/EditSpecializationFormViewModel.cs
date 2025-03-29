using DoctorAppointments.Attributes;

namespace DoctorAppointments.ViewModels.Specialization
{
    public class EditSpecializationFormViewModel : SpecializationFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentImg { get; set; }
        [AllowedExtensions(FileSystem.AllowedExtensions)]
        [MaxFileSize(FileSystem.MaxFileSizeInBytes)]
        public IFormFile? Img { get; set; }
    }
}
