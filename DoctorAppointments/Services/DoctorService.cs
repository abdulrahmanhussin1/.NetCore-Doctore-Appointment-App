namespace DoctorAppointments.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
