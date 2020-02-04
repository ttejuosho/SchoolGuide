using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolGuide.Models;

namespace SchoolGuide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISchoolActions _schoolActions;


        public HomeController(ILogger<HomeController> logger, ISchoolActions schoolActions)
        {
            _logger = logger;
            _schoolActions = schoolActions;
        }

        [AllowAnonymous]
        public IActionResult Index(string searchWord = "")
        {
            if (ModelState.IsValid)
            {
                if (searchWord != null)
                {
                    var model = _schoolActions.SchoolSearch(searchWord);
                    return View(model);
                }
                else
                {
                    return View("Home/Index");
                }
            }
            else
                return View("Index");

        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ErrorViewModel model = new ErrorViewModel
            {
                ExceptionMessage = exceptionDetails.Error.Message,
                ExceptionPath = exceptionDetails.Path,
                StackTrace = exceptionDetails.Error.StackTrace,
                Source = exceptionDetails.Error.Source,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(model);
        }
    }
}
