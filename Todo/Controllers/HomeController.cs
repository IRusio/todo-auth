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
            var a = await HttpContext.GetTokenAsync(IdentityConstants.ApplicationScheme, "access_token");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Add()
        {
            return PartialView();
        }

        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            return PartialView(await GetElementFromId(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async static Task<TodoModel> GetElementFromId(int id)
        {
            var todoListJson = new WebClient().DownloadString($"#/api/todo/{id}");

            return JsonConvert.DeserializeObject<TodoModel>(todoListJson);
        }
    }
}
