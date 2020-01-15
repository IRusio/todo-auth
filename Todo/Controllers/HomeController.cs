using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Todo.Models;

namespace Todo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Add()
        {
            return PartialView();
        }



        [HttpGet]
        [Route("Home/Update/{id}/{userId}")]
        public IActionResult Update(int id, string userId)
        {
            return PartialView(GetElementFromId(id, userId));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static TodoModel GetElementFromId(int id, string userId)
        {
            var todoListJson = new WebClient().DownloadString($"https://localhost:5001/api/todo/{id}/{userId}");

            return JsonConvert.DeserializeObject<TodoModel>(todoListJson);
        }
    }
}
