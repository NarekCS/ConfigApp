using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        UptimeService uptime;

        public HomeController(UptimeService up)
        {
            uptime = up;
        }
        // GET: /<controller>/
        public IActionResult Index(bool throwException=false)
        {
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
