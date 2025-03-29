using DoctorAppointments.Attributes;
using DoctorAppointments.ViewModels.Doctora;

namespace DoctorAppointments.ViewModels.Doctor
{
    public class EditDoctorFormViewModel : DoctorFormViewModel
    {
        public int Id { get; set; }
        public string? CurrentImg { get; set; }

        [AllowedExtensions(FileSystem.AllowedExtensions)]
        [MaxFileSize(FileSystem.MaxFileSizeInBytes)]
        public IFormFile? Img { get; set; }
    }
}
