using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Views
{
    [ValidateAntiForgeryToken]
    [ApiController]
    [Route("/api/todo/")]
    public class TodoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("all/")]
        public JsonResult GetAllTodo()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _context.TodoModel.Where(model => model.TaskOwner == user)?.ToList();
            if (result == null)
                return Json(new List<TodoModel>());
            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult GetTodoViaId(int id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(_context.TodoModel.Where(model => model.Id == id && model.TaskOwner == user)?.ToList());
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpStatusCode> AddToList([FromBody] PreTodoModel preTodoModel)
        {
            var todoModel = preTodoModel as TodoModel;
            todoModel.TaskOwner = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Add(todoModel);
                await _context.SaveChangesAsync();
                return HttpStatusCode.Accepted;
            }

            return HttpStatusCode.BadRequest;
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<HttpStatusCode> RemoveTaskFromList(int id)
        {

            var TaskOwner= User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var todoModel = await _context.TodoModel.FindAsync(id, TaskOwner);
            _context.TodoModel.Remove(todoModel);
            await _context.SaveChangesAsync();
            return HttpStatusCode.Accepted;
        }


        [HttpPut]
        [Route("update/{id}")]
        public async Task<HttpStatusCode> UpdateTask(int id, [FromBody] PreTodoModel data)
        {
            var TaskOwner = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var todoModel = await _context.TodoModel.FindAsync(id, TaskOwner);

            if (data.TaskContent != string.Empty)
                todoModel.TaskContent = data.TaskContent;
            if (data.TaskHeader != string.Empty)
                todoModel.TaskHeader = data.TaskHeader;

            _context.Update(todoModel);
            await _context.SaveChangesAsync();

            return HttpStatusCode.Accepted;
        }
    }
}
