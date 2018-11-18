using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageSnipper_Backend.Controllers
{
    [Route("api/images")]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public ImageController(IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        // POST api/images
        [HttpPost]
        public async Task<IActionResult> Upload(FileModel fileModel)
        {
            if (fileModel?.File == null || fileModel.File.Length <= 0) return BadRequest();

            var file = fileModel.File;
            var path = Path.Combine(_environment.WebRootPath, "images");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (var fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            var domainName = HttpContext.Request.GetUri().GetLeftPart(UriPartial.Authority);
            return Content($"{domainName}/images/{file.FileName}");
        }
    }
}