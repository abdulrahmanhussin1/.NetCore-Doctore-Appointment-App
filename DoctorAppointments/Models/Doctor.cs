﻿namespace DoctorAppointments.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Doctor : BaseModel
    {
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; } = default!;
        [MaxLength(200)]
        public string? Img { get; set; }
    }
}
