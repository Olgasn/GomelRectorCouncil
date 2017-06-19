using GomelRectorCouncil.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Data
{
    public class ExternalFile
    {
        private IHostingEnvironment _environment;
        private IConfiguration _iconfiguration;
        public ExternalFile()
        {

        }
        public ExternalFile(IHostingEnvironment environment, IConfiguration iconfiguration)
        {
            _environment = environment;
            _iconfiguration = iconfiguration;

        }


        public async Task<University> UploadUniversityLogo(University university, IFormFile upload)
        {
            string relativeFileName = "";
            string absoluteFileName = "";
            if (upload != null)
            {
                relativeFileName = _iconfiguration.GetSection("Paths").GetSection("PathToLogos").Value + university.UniversityId.ToString() + upload.FileName;
                university.Logo = relativeFileName;
                absoluteFileName = _environment.WebRootPath + relativeFileName;
                using (var fileStream = new FileStream(absoluteFileName, FileMode.Create))
                {
                    await upload.CopyToAsync(fileStream);
                }

            }                    
            return university;
        }

        public async Task<Rector> UploadRectorPhoto(Rector rector, IFormFile upload)
        {
            string relativeFileName = "";
            string absoluteFileName = "";
            if (upload != null)
            {
                relativeFileName = _iconfiguration.GetSection("Paths").GetSection("PathToPhotos").Value + rector.RectorId.ToString() + upload.FileName;
                rector.Photo = relativeFileName;
                absoluteFileName = _environment.WebRootPath + relativeFileName;
                using (var fileStream = new FileStream(absoluteFileName, FileMode.Create))
                {
                    await upload.CopyToAsync(fileStream);
                }

            }
            return rector;
        }
        public async Task<Document> UploadRectorPhoto(Document document, IFormFile upload)
        {
            string relativeFileName = "";
            string absoluteFileName = "";
            if (upload != null)
            {
                relativeFileName = _iconfiguration.GetSection("Paths").GetSection("PathToDocuments").Value + document.DocumentId.ToString() + upload.FileName;
                document.DocumentURL = relativeFileName;
                absoluteFileName = _environment.WebRootPath + relativeFileName;
                using (var fileStream = new FileStream(absoluteFileName, FileMode.Create))
                {
                    await upload.CopyToAsync(fileStream);
                }

            }
            return document;
        }

    }
}
