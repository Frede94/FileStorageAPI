using FileStorageAPI.Services.Interefaces;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorage _fileStorage;

        public FileStorageController(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            string fileId = await _fileStorage.UploadFileAsync(file, "uploads");

            return Ok(new { FileId = fileId });
        } 

        [HttpGet("{fileId}")]
        [Route("download")]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            try
            {
                Stream stream = await _fileStorage.DownloadFileAsync(fileId);

                return File(stream, "application/octet-stream");
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{fileId}")]
        [Route("delete")]
        public async Task<IActionResult> DeleteFile(string fileId)
        {
            try
            {
                bool result = await _fileStorage.DeleteFileAsync(fileId);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }
    }
}