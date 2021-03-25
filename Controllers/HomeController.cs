using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context { get; set; }
        public HomeController(MovieContext con)
        {
            context = con;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Application()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Application(ApplicationResponse appResponse)
        {
            if (ModelState.IsValid)
            {
                context.Applications.Add(appResponse);
                context.SaveChanges();
            }
            
            return View("Confirmation", appResponse);
        }

        [HttpGet]
        public IActionResult Waitlist()
        {
            return View(context.Applications);
        }

        [HttpPost]
        public IActionResult Remove(ApplicationResponse appResponse)
        {
            context.Applications.Remove(appResponse);
            context.SaveChanges();

            return View("Waitlist", context.Applications);
        }

        [HttpPost]
        public IActionResult Waitlist(int movieid)
        {
            ApplicationResponse Movie = context.Applications.Where(m => m.MovieId == movieid).FirstOrDefault();
            context.Applications.Remove(Movie);
            context.SaveChanges();

            return View("Edit", Movie);
        }

        [HttpPost]
        public IActionResult Update(ApplicationResponse appResponse)
        {
            context.Applications.Add(appResponse);
            context.SaveChanges();
            return View("Waitlist", context.Applications);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
