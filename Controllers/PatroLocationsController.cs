using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using remote_poc_webapi.Helpers;
using remote_poc_webapi.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace remote_poc_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatrolLocationsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PatrolLocationsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PatrolLocations> Create(ItemPatrolLocation item)
        {
            string MobileNumber = item.PhoneNumber ?? "";
            MobileNumber = MobileNumber.Trim();
            PatrolLocations Log = new PatrolLocations();

            Patrol patrol = _context.Patrol.Where(d => d.MobileNumber == MobileNumber).FirstOrDefault();
            
            if (patrol != null)
            {
                Log.Latitude = item.Latitude;
                Log.Longitude = item.Longitude;
                Log.PatrolId = patrol.id;
                Log.Created = DateTime.UtcNow;

                if (!string.IsNullOrEmpty(item.PatrollingNotes))
                {
                    patrol.IsCurrentlyPatrolling = true;
                    patrol.PatrollingNotes = item.PatrollingNotes;
                    _context.Entry(patrol).State = EntityState.Modified;
                }

                _context.PatrolLocations.Add(Log);
                _context.SaveChanges();
            }
            else
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetItemById), new { id = Log.Id }, patrol);
        }
               

        // GET: api/Item/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _context.PatrolLocations.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // GET: api/
        [HttpGet]
        public async Task<IActionResult> GetAllItems(double requestorLatitude, double requestorLongitude)
        {
            var items = await _context.PatrolLocations.Include(d=>d.Patrol).Include(d=>d.Patrol.PatrolType).Where(d=>d.Patrol.IsCurrentlyPatrolling==true).OrderByDescending(d=>d.Created).ToListAsync();
            List<PatrolLocations> list = new List<PatrolLocations>();
            foreach(var item in items)
            {
                var pItem = list.Where(d => d.PatrolId == item.PatrolId).FirstOrDefault();
                if (pItem == null)
                {
                    (double distance,double duration) distanceMatrix = Helper.GetRouteDetails(item.Latitude,item.Longitude,requestorLatitude,requestorLongitude,item.Patrol.PatrolType.TravelMode);
                    item.TimeInMinutes = distanceMatrix.duration;
                    item.DistanceInKm = distanceMatrix.distance;
                    list.Add(item);
                }
            }
            return Ok(list.OrderBy(d=>d.Patrol.PatrolTypeId).ThenBy(d=>d.TimeInMinutes));
        }
    }

    // DTO for input data
    public class ItemPatrolLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? PatrolId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PatrollingNotes { get; set; }
    }
}
