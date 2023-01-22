

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace USER.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userController : ControllerBase
    {
        private IuserService userService;

        public userController(IuserService userService)
        {
            this.userService = userService;
        }


        [HttpPost]
        [Route("[action]")]
        // [Authorize(Policy = "Admin")]
        public ActionResult<String> Login([FromBody] User User)
        {

            if (User.IsAdmin == false)
                return Unauthorized();
            //  var claims = new List<Claim>
            // {
            //     new Claim("type", "Admin"),
            // };
            return "yes";

        }
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {

            List<User> user = userService.GetAll();
            if (user == null)
                return NotFound();
            return user;

        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = userService.Get(id);
            if (user == null)
                return NotFound();
            return user;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult Create(User t)
        {
            userService.Add(t);
            return CreatedAtAction(nameof(Create), new { id = t.Id }, t);

        }





    }



}
