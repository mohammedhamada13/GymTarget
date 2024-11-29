using GymTarget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace GymTarget.Controllers
{
    public class CoachesController : Controller
    {
        
            private readonly ApplicationDbContext _context;
            private readonly IWebHostEnvironment _webHostEnvironment;

        public CoachesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
         {
                _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            var coache = _context.coaches.ToList();

            return View(coache);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Coaches coaches , IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    coaches.Image = @"Images\Products\" + filename + ext;
                }

                _context.coaches.Add(coaches);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

                return View(coaches);
            
        }


       


        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _context.coaches.Find(Id);

            if (item == null)
            {
                return NotFound();
            }


            return View(item);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Coaches coaches, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    coaches.Image = @"Images\Products\" + filename + ext;
                }

                _context.coaches.Update(coaches);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            
                return View(coaches);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coache = _context.coaches.Find(id);

            if (coache == null)
            {
                return NotFound();
            }

            return View(coache);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletecoaches(int? Id)

        {

            var coaches = _context.coaches.Find(Id);
            if (coaches == null)
            {
                return NotFound();
            }
            _context.Remove(coaches);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }


    }
}
