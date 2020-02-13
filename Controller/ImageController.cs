using Microsoft.AspNetCore.Mvc;
using CPope.SimplexNoise;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System;

namespace CPopeWebsite.Controller
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        public ImageController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [Route(Urls.UploadImage)]
        [HttpPost]
        public async Task Post()
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var file in HttpContext.Request.Form.Files)
                {
                    var path = Path.Combine(environment.ContentRootPath, "wwwroot/images");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path = Path.Combine(path, file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }

        [Route(Urls.StaticImage)]
        [HttpGet]
        public IActionResult GetStaticNoise()
        {
            return File(StaticSimplexNoise.NoiseMap2D(4, 1280, 720), "image/jpeg");
        }
    }
}