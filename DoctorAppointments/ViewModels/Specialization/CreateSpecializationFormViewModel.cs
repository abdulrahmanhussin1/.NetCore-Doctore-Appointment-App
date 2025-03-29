using DoctorAppointments.Attributes;

namespace DoctorAppointments.ViewModels.Specialization
{
    public class CreateSpecializationFormViewModel : SpecializationFormViewModel
    {
        [AllowedExtensions(FileSystem.AllowedExtensions)]
        [MaxFileSize(FileSystem.MaxFileSizeInBytes)]
        public IFormFile? Img { get; set; }
    }
}
