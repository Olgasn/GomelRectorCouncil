using GomelRectorCouncil.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Data
{
    public class DocumentExternalFile
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _iconfiguration;
        public DocumentExternalFile()
        {

        }
        public DocumentExternalFile(IWebHostEnvironment environment, IConfiguration iconfiguration)
        {
            _environment = environment;
            _iconfiguration = iconfiguration;

        }


        public async Task<Document> UploadDocument(Document document, IFormFile upload)
        {
            if (upload != null)
            {
                string relativeFileName = _iconfiguration.GetSection("Paths").GetSection("PathToDocuments").Value + document.DocumentId.ToString() + upload.FileName;
                document.DocumentURL = relativeFileName;
                string absoluteFileName = _environment.WebRootPath + relativeFileName;
                using var fileStream = new FileStream(absoluteFileName, FileMode.Create);
                await upload.CopyToAsync(fileStream);

            }
            return document;
        }

    }
}
