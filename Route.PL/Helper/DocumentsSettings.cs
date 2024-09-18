namespace Route.PL.Helper
{
    public static class DocumentsSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Located FolderPath
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            // 2. Get File Name
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
            // 3. Get File Path
            var filePath = Path.Combine(folderPath, fileName);

            // 4. Create File and return its Name
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        public static bool RemoveFile(string file, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            var filePath = Path.Combine(folderPath, file);

            try
            {
                File.Delete(filePath);
                return true;
            }catch (Exception)
            {
                return false;
            }
        }
    }
}
