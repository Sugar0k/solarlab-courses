using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Image
{
    public partial class ImageController
    {
        [HttpPost]
        [Route("create")]
        public string Create(IFormFile file)
        {
            string path = "Files";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (file.Length > 0)
            {
                string guid = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using Stream fileStream = new FileStream(Path.Combine(path, guid), FileMode.Create);
                file.CopyTo(fileStream);
                _db.Images.Add(new Models.Image
                {
                    FileName = file.FileName,
                    Guid = guid,
                    FilePath = path
                });
                _db.SaveChangesAsync();
                return guid;
            }

            return null;
        }
        
    }
}