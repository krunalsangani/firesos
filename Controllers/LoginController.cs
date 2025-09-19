using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using remote_poc_webapi.Helpers;
using remote_poc_webapi.Model;
using System.Net;

namespace remote_poc_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoginController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Validate(LoginModel model)
        {
            var profile = _context.UserProfiles.Where(d => d.PhoneNumber == model.PhoneNumber.Trim().Replace(" ", string.Empty) && d.Last4Aadhar == model.Last4Aadhar);
            if (profile !=null && profile.Count() > 0 )
            {
                var user = profile.FirstOrDefault();
                if (user != null)
                {
                    UserModel m = new UserModel();
                    m.Id = user.Id;
                    m.Name=user.Name;
                    m.Venue = user.Address;
                    return Ok(m);
                }
            }
            return NotFound();
        }
    }

}
