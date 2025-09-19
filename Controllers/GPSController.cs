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

namespace remote_poc_webapi.Controllers
{


    [ApiController]
    [Route("/api/gps")]
    public class GPSController : ControllerBase
    {
        private readonly MyDbContext _context;

        public GPSController(MyDbContext context) 
        {
            _context = context;

        }
        public IActionResult ReceiveGps(string hexData)
        {
            /*var parsedData = ParseGT06(hexData);
            if (parsedData == null)
            {
                return BadRequest(new { status = "error", message = "Invalid data" });
            }

            var (latitude, longitude) = parsedData.Value;*/
            var latitude = 1;
            var longitude = 2;

            //return Ok(new { status = "success", latitude, longitude });
            return Ok(new { status = "success", latitude, longitude });
        }

        [HttpPost]
        [Consumes("application/octet-stream")]
        public IActionResult Post()
        {
            var req = HttpContext.Request;
            using (var ms = new MemoryStream())
            {
                req.Body.CopyToAsync(ms).Wait();

                var hexData = BitConverter.ToString(ms.ToArray()).Replace("-", "");
                Console.WriteLine(hexData);
                //System.IO.File.WriteAllText("gps.txt", hexData);
                return ReceiveGps(hexData);
            }
        }

        [HttpGet]
        public IActionResult Get(string latitude, string longitude,int Id)
        {
            DroneLocation patrolLocation = new DroneLocation();
            patrolLocation.Latitude = Convert.ToDouble(latitude);
            patrolLocation.Longitude = Convert.ToDouble(longitude) ;
            
            patrolLocation.DroneId = Id;
            patrolLocation.Created = DateTime.UtcNow;

            _context.DroneLocation.Add(patrolLocation);
            _context.SaveChanges();
            return Ok("Location recorded");
        }

        private (double latitude, double longitude)? ParseGT06(string hexData)
        {
            try
            {
                byte[] packet = ConvertHexStringToByteArray(hexData);
                if (packet.Length < 15)
                {
                    return null;
                }

                int latRaw = BitConverter.ToInt32(new byte[] { packet[10], packet[9], packet[8], packet[7] }, 0);
                int lonRaw = BitConverter.ToInt32(new byte[] { packet[14], packet[13], packet[12], packet[11] }, 0);

                DroneLocation  patrolLocation    = new DroneLocation();
                patrolLocation.Latitude = latRaw / 1000000.0;
                patrolLocation.Longitude = lonRaw / 1000000.0;
                patrolLocation.Drone = _context.DeployedDrones.Where(d => d.Id == 3).FirstOrDefault();
                patrolLocation.DroneId = patrolLocation.Drone.Id;
                patrolLocation.Created = DateTime.UtcNow;
                _context.DroneLocation.Add(patrolLocation);
                _context.SaveChanges();

                double latitude = latRaw / 1000000.0;
                double longitude = lonRaw / 1000000.0;

                return (latitude, longitude);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parsing error: {ex.Message}");
                return null;
            }
        }

        private byte[] ConvertHexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}