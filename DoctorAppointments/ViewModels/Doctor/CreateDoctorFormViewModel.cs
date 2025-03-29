using DoctorAppointments.Attributes;
using DoctorAppointments.ViewModels.Doctora;

namespace DoctorAppointments.ViewModels.Doctor
{
    public class CreateDoctorFormViewModel : DoctorFormViewModel
    {
        [AllowedExtensions(FileSystem.AllowedExtensions)]
        [MaxFileSize(FileSystem.MaxFileSizeInBytes)]
        public IFormFile? Img { get; set; }
    }
}
