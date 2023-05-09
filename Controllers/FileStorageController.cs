using Microsoft.AspNetCore.Mvc;
using FileStorageAPI.Services.Interefaces;
using System.IO;

namespace FileStorageAPI.Controllers
{
    [ApiController]
    [Route("api/file-storage")]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorage _fileStorage;

        public FileStorageController(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string location)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            if (string.IsNullOrEmpty(location))
            {
                return BadRequest("No location was specified for the uploaded file.");
            }

            var fileId = await _fileStorage.UploadFileAsync(file, location);

            return Ok(new { fileId });
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> Download(string fileId)
        {
            var stream = await _fileStorage.DownloadFileAsync(fileId);

            if (stream == null)
            {
                return NotFound();
            }

            // Return the file as an attachment with the correct content type
            return File(fileStream, file.ContentType, file.FileName);
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> Delete(string fileId)
        {
            var success = await _fileStorage.DeleteFileAsync(fileId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}