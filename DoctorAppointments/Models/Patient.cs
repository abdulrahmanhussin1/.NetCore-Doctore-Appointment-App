namespace DoctorAppointments.Models
{
    public class Patient : BaseModel
    {
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}
