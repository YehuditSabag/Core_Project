

using System;
using System.Collections.Generic;
using System.Linq;


using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoList.Services;
using TodoList.Interfaces;
using TodoList.Models;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {

        ITodoService TodoService;
        public TodoController(ITodoService TodoService)
        {
            this.TodoService = TodoService;
        }


        [HttpGet]
        public ActionResult<List<Mylist>> GetAll() 
        {
             
            var task = TodoService.GetAll();
            if (task == null)
                return NotFound();
            return task;
       

        }
         

        [HttpGet("{id}")]
        public ActionResult<Mylist> Get(int id)
        {
            var task = TodoService.Get(id);
            if (task == null)
                return NotFound();
            return task;
        }




        [HttpPost]
        public IActionResult Create(Mylist t)
        {
            TodoService.Add(t);
            return CreatedAtAction(nameof(Create), new { id = t.Id }, t);

        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, Mylist t)
        {
            if (id != t.Id)
                return BadRequest();
            //    
            var res = TodoService.Get(id);
            if (res is null)
                return NotFound();
            TodoService.Update(t);
            return NoContent();
            // 

        }

         [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = TodoService.Get(id);
            if (task is null)
                return  NotFound();

            TodoService.Delete(id);

            return Content(TodoService.Count.ToString());
        }
    }
}
