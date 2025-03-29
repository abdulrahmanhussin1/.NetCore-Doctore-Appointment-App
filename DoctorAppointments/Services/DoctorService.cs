using Microsoft.Data.SqlClient;

namespace DoctorAppointments.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private const string StorageDirectory = "images/doctors";

        public DoctorService(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors
                .Include(s => s.Specialization)
                .AsNoTracking()
                .ToList();
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Doctors
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .OrderBy(c => c.Text)
                    .AsNoTracking()
                    .ToList();
        }

        public Doctor? GetById(int id)
        {
            return _context.Doctors
                .AsNoTracking()
                .SingleOrDefault(d => d.Id == id);
        }

        public async Task<(bool Success, string? ErrorMessage)> Store(CreateDoctorFormViewModel model)
        {
            var imgName = model.Img is not null
               ? await _storageService.PutFileAs(StorageDirectory, model.Img, Guid.NewGuid() + Path.GetExtension(model.Img.FileName))
               : null;
            try
            {
                var doctor = new Doctor
                {
                    Name = model.Name,
                    SpecializationId = model.SpecializationId,
                    Img = imgName
                };

                await _context.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return (false, "A doctor with this Name already exists.");
            }
            catch (Exception)
            {
                return (false, "An error occurred while saving the doctor.");
            }
        }


        public async Task<(Doctor? UpdatedDoctor, string? ErrorMessage)> Update(EditDoctorFormViewModel model)
        {
            try
            {
                var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == model.Id);
                if (doctor is null) return (null, "Doctor not found.");

                var oldImg = doctor.Img;
                var hasNewImg = model.Img is not null;

                bool nameExists = await _context.Doctors.AnyAsync(d => d.Name == model.Name && d.Id != model.Id);
                if (nameExists)
                {
                    return (null, "A doctor with this name already exists.");
                }

                doctor.Name = model.Name;
                doctor.SpecializationId = model.SpecializationId;


                if (hasNewImg)
                {
                    doctor.Img = await _storageService.PutFileAs(
                        StorageDirectory,
                        model.Img!,
                        Guid.NewGuid() + Path.GetExtension(model.Img.FileName)
                    );
                }

                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    if (hasNewImg && !string.IsNullOrEmpty(oldImg))
                    {
                        _storageService.Delete(oldImg);
                    }

                    return (doctor, null);
                }
                else
                {
                    if (hasNewImg && !string.IsNullOrEmpty(doctor.Img))
                    {
                        _storageService.Delete(doctor.Img);
                    }

                    return (null, "No changes were made to the doctor.");
                }
            }
            catch (DbUpdateException)
            {
                return (null, "An error occurred while updating the doctor.");
            }
        }
    }
    }
