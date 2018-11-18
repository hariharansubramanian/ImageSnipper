using Microsoft.AspNetCore.Http;

namespace ImageSnipper_Backend.Model
{
    public class FileModel
    {
        public IFormFile File { get; set; }
    }
}