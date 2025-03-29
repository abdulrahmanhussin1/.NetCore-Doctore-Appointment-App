namespace DoctorAppointments.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _rootPath;
        private readonly IWebHostEnvironment _environment;

        public StorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
            _rootPath = Path.Combine(_environment.WebRootPath, "assets");
        }

        public async Task<string> PutFileAs(string directory, IFormFile file, string fileName)
        {
            var folderPath = Path.Combine(_rootPath, directory);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"{directory}/{fileName}"; // Return relative path
        }

        public string Url(string filePath)
        {
            return $"/assets/{filePath}"; // Return web URL
        }

        public bool Exists(string filePath)
        {
            var fullPath = Path.Combine(_rootPath, filePath);
            //check if fiel exist in path 

            return File.Exists(fullPath);
        }

        public string GetImageUrl(string? filePath, string defaultImage = "images/default-avatar.jpg")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return Url(defaultImage); // Use default if no path provided
            }

            var fullPath = Path.Combine(_rootPath, filePath);

            // Return the existing file path or default image
            return File.Exists(fullPath) ? Url(filePath) : Url(defaultImage);
        }


        public void Delete(string filePath)
        {
            var fullPath = Path.Combine(_rootPath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
