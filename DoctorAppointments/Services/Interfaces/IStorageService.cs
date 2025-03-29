namespace DoctorAppointments.Services.Interfaces
{
    public interface IStorageService
    {
        Task<string> PutFileAs(string directory, IFormFile file, string fileName);
        string Url(string filePath);
        bool Exists(string filePath);
        void Delete(string filePath);
    }
}
