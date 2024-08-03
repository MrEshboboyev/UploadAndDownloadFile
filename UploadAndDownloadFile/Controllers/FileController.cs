using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UploadAndDownloadFile.Models;

namespace UploadAndDownloadFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadModel model)
        {
            if (model.File == null && model.File.Length == 0)
            {
                return BadRequest("Invalid File");
            }

            var folderName = Path.Combine("Resources", "AllFiles");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = model.File.FileName;
             
            // full name of file : "Resources/AllFiles" + "upload.x"
            var fullPath = Path.Combine(pathToSave, fileName);

            // database path for upload file
            var dbPath = Path.Combine(folderName, fileName);

            // already exists in this name file
            if (System.IO.File.Exists(fullPath))
            {
                return BadRequest("File already exists!");
            }

            // copy to upload file to fullPath with FileStream
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }


            return Ok( new { dbPath });
        }
    }
}
