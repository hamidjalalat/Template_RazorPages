using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Features.Common;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace Server.Pages.Features.Common
{
    public class UploadImageAxiosModel : PageModel
    {
        public void OnGet()
        {
        }
        public JsonResult OnPost(Microsoft.AspNetCore.Http.IFormFile? file)
        {
            string folderName = Path.Combine("wwwroot", "images", "predefined_slides");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string dbPath = "";

            var result = CheckFileValidation(file: file);

            if (result)
            {
                string fileNameContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fileName = Guid.NewGuid().ToString() + "." + fileNameContent.Substring(fileNameContent.Length - 3);

                string fullPath = Path.Combine(pathToSave, fileName);
                dbPath = Path.Combine(folderName, fileName);

                dbPath = dbPath.Replace("\\", "/");

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return new JsonResult(dbPath);
        }
 
        private bool  CheckFileValidation(Microsoft.AspNetCore.Http.IFormFile? file)
        {
            if (file is null)
            {
                return false;
            }

            if (file.Length == 0)
            {
        
                return false;
            }

            return true;
        }
    
    }
}
