

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using user.Interfaces;
using user.Models;
using user.Services;

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
            
            
                claims.Add( new Claim("type", "User"));
            
                claims.Add(new Claim("Id", getuser.Id.ToString()));
            
            
            var token = Services.TokenService.GetToken(claims);

            return new OkObjectResult(Services.TokenService.WriteToken(token));
        }
        
       
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public ActionResult<List<User>> GetAll()
        {

            List<User> user = userService.GetAll();
            if (user == null)
                return NotFound();
            return user;

        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult<User> Get()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var user = userService.Get(TokenService.decode(token));
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPost("{user}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Create([FromBody] User user)
        {
            userService.Add(user);
            return CreatedAtAction(nameof(Create), new { Id = user.Id }, user);

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
