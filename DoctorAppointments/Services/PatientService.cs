using Microsoft.Data.SqlClient;

namespace DoctorAppointments.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Patients
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .OrderBy(p => p.Text)
                    .AsNoTracking()
                    .ToList();
        }

        public Patient? GetById(int id)
        {
            return _context.Patients
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

        public async Task<(bool Success, string? ErrorMessage)> Store(CreatePatientFormViewModel model)
        {
            try
            {
                var patient = new Patient
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email
                };

                await _context.AddAsync(patient);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return (false, "A patient with this email already exists.");
            }
            catch (Exception)
            {
                return (false, "An error occurred while saving the patient.");
            }
        }


        public async Task<(Patient? UpdatedPatient, string? ErrorMessage)> Update(EditPatientFormViewModel model)
        {
            try
            {
                var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == model.Id);
                if (patient is null) return (null, "Patient not found.");

                // Ensure no duplicate email exists (excluding the current patient's email)
                bool emailExists = await _context.Patients.AnyAsync(p => p.Email == model.Email && p.Id != model.Id);
                if (emailExists)
                {
                    return (null, "A patient with this email already exists.");
                }

                patient.Name = model.Name;
                patient.Phone = model.Phone;
                patient.Email = model.Email;

                await _context.SaveChangesAsync();
                return (patient, null);
            }
            catch (DbUpdateException)
            {
                return (null, "An error occurred while updating the patient.");
            }
        }
    }
}
