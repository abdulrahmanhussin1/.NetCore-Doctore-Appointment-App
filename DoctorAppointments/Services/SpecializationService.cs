namespace DoctorAppointments.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private const string StorageDirectory = "images/specializations";

        public SpecializationService(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
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
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                    .OrderBy(s => s.Text)
                    .AsNoTracking()
                    .ToList();
        }

        public Specialization? GetById(int id)
        {
            return _context.Specializations
                .Include(d => d.Doctors)
                .AsNoTracking()
                .SingleOrDefault(s => s.Id == id);
        }

        public async Task Store(CreateSpecializationFormViewModel model)
        {
            var imgName = model.Img is not null
                ? await _storageService.PutFileAs(StorageDirectory, model.Img, Guid.NewGuid() + Path.GetExtension(model.Img.FileName))
                : null;

            var specialization = new Specialization
            {
                Name = model.Name,
                Description = model.Description,
                Img = imgName
            };

            await _context.AddAsync(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task<Specialization?> Update(EditSpecializationFormViewModel model)
        {
            var specialization = await _context.Specializations.SingleOrDefaultAsync(d => d.Id == model.Id);
            if (specialization is null) return null;

            var oldImg = specialization.Img;
            var hasNewImg = model.Img is not null;

            specialization.Name = model.Name;
            specialization.Description = model.Description;

            if (hasNewImg)
            {
                specialization.Img = await _storageService.PutFileAs(StorageDirectory, model.Img!, Guid.NewGuid() + Path.GetExtension(model.Img.FileName));
            }

            var effectedRows = await _context.SaveChangesAsync();

            if (effectedRows > 0)
            {
                if (hasNewImg && !string.IsNullOrEmpty(oldImg))
                {
                    _storageService.Delete(oldImg);
                }
                return specialization;
            }
            else
            {
                if (hasNewImg && !string.IsNullOrEmpty(specialization.Img))
                {
                    _storageService.Delete(specialization.Img);
                }
                return null;
            }
        }
    }
}
