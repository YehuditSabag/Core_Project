

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user.Interfaces;
using user.Models;

namespace user.Controllers
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
        public ActionResult<String> Login([FromBody] User User)
        {
             var claims = new List<Claim>();
             var getuser=userService.GetAll().FirstOrDefault(c=>c.Username ==User.Username && c.Password == User.Password);
              if (getuser == null)
                return Unauthorized();
          
             if(getuser.IsAdmin)
             {
                 claims.Add(new Claim("type","Admin"));
             }
            else
            {
                claims.Add( new Claim("type", "User"));
            }
                claims.Add(new Claim("Id", getuser.Id.ToString()));
            
            
            var token = services.TaskTokenService.GetToken(claims);

            return new OkObjectResult(services.TaskTokenService.WriteToken(token));
        }
        
       
        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
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

        [HttpDelete("{id}")]             
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            var user = userService.Get(id);
            if (user is null)
                return  NotFound();

            userService.Delete(id);

            return Content(userService.Count.ToString());
        }


    }



}
