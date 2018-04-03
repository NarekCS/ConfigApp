using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        UptimeService uptime;
        ILogger<HomeController> logger;

        public HomeController(UptimeService up, ILogger<HomeController> log)
        {
            uptime = up;
            logger = log;
        }
        // GET: /<controller>/
        public IActionResult Index(bool throwException=false)
        {
            logger.LogDebug($"Handled {Request.Path} at uptime {uptime.Uptime}");
            if (throwException)
                throw new NullReferenceException();

            return View(new Dictionary<string, string>
            {
                ["Message"] = "This is the Index action",
                ["Uptime"] = $"{uptime.Uptime}ms"
            });
        }

        public ViewResult Error() => View(nameof(Index), new Dictionary<string, string> { ["Message"] = "This is the error action" });
    }
}
