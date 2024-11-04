using Book_Management_System.Models;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly TempDB _context;

        public HomeController(TempDB context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,Location = ResponseCacheLocation.None,NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
