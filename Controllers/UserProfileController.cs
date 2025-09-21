using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using remote_poc_webapi.Helpers;
using remote_poc_webapi.Model;

namespace remote_poc_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UserProfileController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserProfileController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserProfile> Create(UserProfile profile)
        {
            UserProfile profile1 = new UserProfile(profile.Name, profile.PhoneNumber, profile.Latitude, profile.Latitude, profile.Address, profile.EmergencyContact, profile.EmergencyContactPhoneNumber, profile.EmergencyContactRelationship);
            _context.UserProfiles.Add(profile1);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetItemById), new { id = profile1.Id }, profile1);
        }


        // GET: api/Item/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _context.UserProfiles.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // GET: api/Item/{id}
        [HttpGet]
        public async Task<IActionResult> GetAllItems(double requestorLatitude, double requestorLongitude)
        {
            var items = await _context.UserProfiles.ToListAsync();
            return Ok(items.OrderBy(d => d.Name));
        }
    }

}
