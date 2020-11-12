using GomelRectorCouncil.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Data
{
    public class UniversityExternalFile
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _iconfiguration;
        public UniversityExternalFile()
        {

        }
        public UniversityExternalFile(IWebHostEnvironment environment, IConfiguration iconfiguration)
        {
            _environment = environment;
            _iconfiguration = iconfiguration;

        }


        public async Task<University> UploadUniversityWithLogo(University university, IFormFile upload)
        {
            if (upload != null)
            {
                string relativeFileName = _iconfiguration.GetSection("Paths").GetSection("PathToLogos").Value + university.UniversityId.ToString() + upload.FileName;
                university.Logo = relativeFileName;
                string absoluteFileName = _environment.WebRootPath + relativeFileName;
                using var fileStream = new FileStream(absoluteFileName, FileMode.Create);
                await upload.CopyToAsync(fileStream);

            }                    
            return university;
        }

       

    }
}
