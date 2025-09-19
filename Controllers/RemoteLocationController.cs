using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using remote_poc_webapi.Model;

namespace remote_poc_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class RemoteLocationController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RemoteLocationController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RequestLog> Create(ItemDto item)
        {
            RequestLog Log= new RequestLog();
            Log.Latitude = item.Latitude;
            Log.Longitude = item.Longitude;
            Log.HelpText = item.HelpText;
            Log.UserProfile = _context.UserProfiles.Where(d=>d.Id==item.UserId).FirstOrDefault();
            Log.Created = DateTime.UtcNow;

            _context.RequestLog.Add(Log);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetItemById), new { id = Log.Id }, Log);
        }
               

        // GET: api/Item/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            
            var item = await _context.RequestLog.Include(d=>d.UserProfile).Where(d=>d.Id==id).FirstOrDefaultAsync();
            if (item == null)
                return NotFound();


            return Ok(item);
        }

        // GET: api/Item/{id}
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _context.RequestLog.OrderByDescending(d=>d.Created).Take(5).ToListAsync();
            return Ok(items);
        }
    }
}
