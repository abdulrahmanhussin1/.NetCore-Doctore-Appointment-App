namespace DoctorAppointments.Models
{
    public class Specialization : BaseModel
    {
        public string Description { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
