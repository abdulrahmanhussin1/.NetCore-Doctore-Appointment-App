using System.ComponentModel.DataAnnotations;

namespace DoctorAppointments.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty; 
    }
}
