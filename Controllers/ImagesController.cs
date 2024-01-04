

using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace British_Kingdom_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ImagesController(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImages(List<IFormFile> files, [FromQuery] string directory)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest("No files were selected.");
                }

                List<string> uploadedFilePaths = new List<string>();

                foreach (var file in files)
                {
                    if (file.Length == 0)
                        continue;

                    string uploadsFolder = Path.Combine(_environment.WebRootPath, $"assets/{directory}");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                    string extension = Path.GetExtension(file.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString()+ extension;

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    uploadedFilePaths.Add(filePath);
                }

                return Ok(new { uploadedFilePaths });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
