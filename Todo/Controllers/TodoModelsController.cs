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
        [Route("all/{id}")]
        public JsonResult GetAllTodo(string id)
        {
            var result = _context.TodoModel.Where(model => model.TaskOwner == id).ToList();
            if (result == null)
                return Json(new List<TodoModel>());
            return Json(result);
        }

        [HttpGet]
        [Route("{id}/{userId}")]
        public JsonResult GetTodoViaId(int id, string userId)
        {
            var user = userId;
            return Json(_context.TodoModel.Where(model => model.Id == id && model.TaskOwner == userId)?.First());
        }

        [HttpPost]
        [Route("add/{userId}")]
        public async Task<HttpStatusCode> AddToList([FromBody] PreTodoModel preTodoModel, string userId)
        {
            if (userId == null)
                return HttpStatusCode.BadRequest;

            TodoModel todoModel = new TodoModel(preTodoModel) {TaskOwner = userId};
            if (ModelState.IsValid)
            {
                _context.Add(todoModel);
                await _context.SaveChangesAsync();
                return HttpStatusCode.Accepted;
            }

            return HttpStatusCode.BadRequest;
        }

        [HttpDelete]
        [Route("remove/{id}/{userId}")]
        public async Task<HttpStatusCode> RemoveTaskFromList(int id, string userId)
        {

            var TaskOwner = userId;
            
            var todoModel = await _context.TodoModel.FindAsync(id);
            if (todoModel.TaskOwner != userId)
                return HttpStatusCode.BadRequest;
            _context.TodoModel.Remove(todoModel);
            await _context.SaveChangesAsync();
            return HttpStatusCode.Accepted;
        }


        [HttpPut]
        [Route("update/{id}/{userId}")]
        public async Task<HttpStatusCode> UpdateTask(int id, [FromBody] PreTodoModel data, string userId)
        {
            var TaskOwner = userId;
            var todoModel = await _context.TodoModel.FindAsync(id);

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
