using FileStorageAPI.Services.Interefaces;

namespace FileStorageAPI.Services
{
    public class FileStorage : IFileStorage
    {
        public async Task<string> UploadFileAsync(IFormFile file, string location)
        {
            // Create directory if it doesn't exist
            Directory.CreateDirectory(location);

            // Generate unique file name
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Construct full file path
            string filePath = Path.Combine(location, fileName);

            // Copy uploaded file to file path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return unique identifier for uploaded file
            return fileName;
        }

        public async Task<Stream> DownloadFileAsync(string fileId)
        {
            // Construct full file path
            string filePath = Path.Combine("uploads", fileId);

            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", fileId);
            }

            // Open file stream
            var fileStream = new FileStream(filePath, FileMode.Open);

            // Return stream containing contents of downloaded file
            return await Task.FromResult((Stream)fileStream);
        }

        public async Task<bool> DeleteFileAsync(string fileId)
        {
            // Construct full file path
            string filePath = Path.Combine("uploads", fileId);

            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", fileId);
            }

            // Delete file
            File.Delete(filePath);

            // Return boolean indicating whether the file was successfully deleted
            return await Task.FromResult(true);
        }
    }
}