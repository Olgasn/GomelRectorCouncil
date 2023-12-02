using GomelRectorCouncil.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Data
{
    public class RectorExternalFile
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _iconfiguration;
        public RectorExternalFile()
        {

        }
        public RectorExternalFile(IWebHostEnvironment environment, IConfiguration iconfiguration)
        {
            _environment = environment;
            _iconfiguration = iconfiguration;

        }

        public async Task<Rector> UploadRectorWithPhoto(Rector rector, IFormFile upload)
        {
            if (upload != null)
            {
                string relativeFileName = _iconfiguration.GetSection("Paths").GetSection("PathToPhotos").Value + rector.RectorId.ToString() + upload.FileName;
                rector.Photo = relativeFileName;
                string absoluteFileName = _environment.WebRootPath + relativeFileName;
                using var fileStream = new FileStream(absoluteFileName, FileMode.Create);
                await upload.CopyToAsync(fileStream);

            }
            return rector;
        }


    }
}

