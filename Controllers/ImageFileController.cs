using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Threading;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Linq;
using remote_poc_webapi.Model;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Routing.Constraints;

namespace remote_poc_webapi.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ImageFileController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "vip\\upload");

        public ImageFileController(MyDbContext context) 
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
           var items = _context.VIP_Request.Where(d=>d.IsProcessed == false).ToList();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                if (!Directory.Exists(_uploadFolder))
                {
                    Directory.CreateDirectory(_uploadFolder);
                }

                string filePath = Guid.NewGuid().ToString();
                filePath += file.FileName.LastIndexOf(".") > 0 ? file.FileName.Substring(file.FileName.LastIndexOf(".")) : "";
                string fileName = filePath;
                
                Path.Combine(_uploadFolder, filePath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                
                _context.VIP_Request.Add(new VIP_Request { Created= DateTime.Now, IsProcessed= false, NumberPlate=string.Empty, FileLocation = "/vip/upload/" + fileName });
                _context.SaveChanges();

                return Ok(new { message = "File uploaded successfully", filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}