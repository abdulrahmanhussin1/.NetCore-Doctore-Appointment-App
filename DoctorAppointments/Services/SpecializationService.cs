namespace DoctorAppointments.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ApplicationDbContext _context;
        public SpecializationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Specialization> GetAll()
        {
            return _context.Specializations
                .Include(d => d.Doctors)
                .AsNoTracking()
                .ToList();
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Specializations
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .OrderBy(c => c.Text)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
