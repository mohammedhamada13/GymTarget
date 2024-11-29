using GymTarget.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymTarget.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.coaches.ToList();
            return View(products);
        }

        public IActionResult Details(int ProductId)
        {

            ProductId = ProductId;
           var  Coaches = _context.coaches.FirstOrDefault(v => v.Id == ProductId);
                
            return View(Coaches);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
