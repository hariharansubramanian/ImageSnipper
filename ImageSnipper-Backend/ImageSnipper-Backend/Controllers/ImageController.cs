using System;
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
    [EnableCors("CORSPolicy")]
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

        /// POST api/images/crop
        /// <summary>
        /// Extracts image from formdata, creates image into 'http://host-name:port/uploadedImages'
        /// Runs python script to crop image
        /// </summary>
        /// <returns>
        /// Web hosted string URL of cropped image
        /// </returns>
        [HttpPost]
        [Route("crop/")]
        public async Task<IActionResult> CropImage(FileModel fileModel)
        {
            try
            {
                if (fileModel?.File == null || fileModel.File.Length <= 0) return BadRequest();

                var imageFile = fileModel.File;
                var imageName = imageFile.FileName;

                // create image into <http://localhost:port/uploadedImages>
                var uploadPath = Path.Combine(_environment.WebRootPath, _configuration.GetValue<string>("ImagesPath:Uploaded"));
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                using (var fileStream = new FileStream(Path.Combine(uploadPath, imageName), FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream); // create file from stream
                }

                // execute python script
                ScriptRunner.ExecutePythonScript(_environment, _configuration, imageName);

                // get cropped image from <http:localhost:port/processedImages>
                var croppedImageDir = _configuration.GetValue<string>("ImagesPath:Processed");
                var croppedImagePath = Path.Combine(_environment.WebRootPath, croppedImageDir, imageName);

                // if processed file does not exist, throw 404 NOT FOUND
                if (!new FileInfo(croppedImagePath).Exists) return NotFound("Image was not processed.");

                var websiteName = HttpContext.Request.GetUri().GetLeftPart(UriPartial.Authority);
                var croppedImageUrl = $"{websiteName}/{croppedImageDir}/{imageName}";

                return Content(croppedImageUrl); // returns web hosted image url of the cropped image
            }
            catch (Exception e)
            {
                // NOTE: Bad practice to expose internal errors as http response to browser
                // but intentionaly wrote this incase you need to know whats going wrong via browser network console
                // hopefully you wont need this :)
                return NotFound(e.Message);
            }
        }
    }
}