using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImageSnipper_Backend.Model;
using ImageSnipper_Backend.Utils;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ImageSnipper_Backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/images")]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ImageController(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration;
        }

        // POST api/images
        [HttpPost]
        public async Task<IActionResult> CropImage(FileModel fileModel)
        {
            try
            {
                if (fileModel?.File == null || fileModel.File.Length <= 0) return BadRequest();

                var file = fileModel.File;
                var imageName = file.FileName;
                var uploadedImagesPath = _configuration.GetValue<string>("ImagesPath:Uploaded");
                var path = Path.Combine(_environment.WebRootPath, uploadedImagesPath);

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                using (var fs = new FileStream(Path.Combine(path, imageName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }

                var croppedImage = ScriptService.ExecutePythonScript(_environment, _configuration, imageName);
                return Content(croppedImage);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}