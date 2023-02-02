using Microsoft.AspNetCore.Mvc;
using TodoList.Interfaces;
using TodoList.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using user.Interfaces;
using user.Services;

namespace TodoList.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {

        private ITodoService TodoService;
        public TodoController(ITodoService TodoService)
        {
            this.TodoService = TodoService;
        }


        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<List<Mylist>> GetAll()
        {

            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

           
            int Tokenid = TokenService.decode(token);

            var task = TodoService.GetAll(Tokenid);

            return task;


        }




        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult<Mylist> Get(int id)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

          
            int Tokenid = TokenService.decode(token);
            var task = TodoService.Get(id, Tokenid);
            if (task == null)
                return NotFound();
            return task;
        }




        [HttpPost]
        [Authorize(Policy = "User")]
        public IActionResult Create(Mylist t)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
          
            t.UserId = TokenService.decode(token);
            TodoService.Add(t);
            return CreatedAtAction(nameof(Create), new { id = t.Id }, t);

        }
        [HttpPut("{id}")]
         [Authorize(Policy = "User")]
        public ActionResult Update(int id, Mylist t)
        {
            if (id != t.Id)
                return BadRequest();
            //    
            // var res = TodoService.Get(id);
            // if (res is null)
            //     return NotFound();
            TodoService.Update(t);
            return NoContent();
            // 

        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult Delete(int id)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
          
            var t =TokenService.decode(token);
            var task = TodoService.Get(id, t);
            if (task is null)
                return NotFound();

            TodoService.Delete(id,t);

            return Content(TodoService.Count.ToString());
        }
    }
}
