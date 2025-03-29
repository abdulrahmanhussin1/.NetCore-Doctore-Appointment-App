namespace DoctorAppointments.Models
{
    public class Specialization : BaseModel
    {
        public string? Description { get; set; } 
        public string? Img { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
