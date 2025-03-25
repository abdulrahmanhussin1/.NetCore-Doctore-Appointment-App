using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DoctorAppointments.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Specialization)
            .WithMany(s => s.Doctors)
            .HasForeignKey(d => d.SpecializationId);

        modelBuilder.Entity<Specialization>()
            .HasData(new Specialization[]
            {
                new Specialization { Id = 1, Name = "Cardiology" },
                new Specialization { Id = 2, Name = "Dermatology" },
                new Specialization { Id = 3, Name = "Neurology" },
                new Specialization { Id = 4, Name = "Pediatrics" },
                new Specialization { Id = 5, Name = "Psychiatry" },
                new Specialization { Id = 6, Name = "Radiology" }
            });

        modelBuilder.Entity<Doctor>()
            .HasData(new Doctor[]
            {
                new Doctor { Id = 1, Name = "Dr. John Doe", SpecializationId = 1, Img = "/doctors/doctor1.jpg" },
                new Doctor { Id = 2, Name = "Dr. Jane Smith", SpecializationId = 2, Img = "/doctors/doctor2.jpg" },
                new Doctor { Id = 3, Name = "Dr. Richard Roe", SpecializationId = 3, Img = "/doctors/doctor3.jpg" },
                new Doctor { Id = 4, Name = "Dr. Alice Johnson", SpecializationId = 4, Img = "/doctors/doctor4.jpg" }
            });

        modelBuilder.Entity<Patient>().HasData(
               new Patient { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Phone = "123-456-7890" },
               new Patient { Id = 2, Name = "Jane Smith", Email = "janesmith@example.com", Phone = "987-654-3210" },
               new Patient { Id = 3, Name = "Richard Roe", Email = "richardroe@example.com", Phone = "456-789-1234" },
               new Patient { Id = 4, Name = "Alice Johnson", Email = "alicejohnson@example.com", Phone = "789-123-4567" }
           );

        base.OnModelCreating(modelBuilder);
    }
}
