﻿namespace DoctorAppointments.Config
{
    public class FileSystem
    {
        public const string ImagesPath = "/assets/images";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
