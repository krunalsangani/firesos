using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using remote_poc_webapi.Helpers;

namespace remote_poc_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class DroneController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DroneController(MyDbContext context)
        {
            _context = context;
        }

        /*[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Drone> Create(ItemDrone item)
        {
            
            return CreatedAtAction(nameof(GetItemById), new { id = 1 }, null);
        }*/
               

        // GET: api/Item/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _context.DeployedDrones.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // GET: api/Item/{id}
        [HttpGet]
        public async Task<IActionResult> GetAllItems(double requestorLatitude,double requestorLongitude)
        {
            var items = await _context.DeployedDrones.ToListAsync();
            foreach(var item in items)
            {
                (double distance,double duration) distanceMatrix = Helper.GetRouteDetails(item.Latitude, item.Longitude, requestorLatitude, requestorLongitude, "DRIVE");
                item.TimeInMinutes = distanceMatrix.duration;
                item.DistanceInKms = distanceMatrix.distance;
            }    
            return Ok(items.OrderBy(d=>d.DistanceInKms));
        }
    }

}
