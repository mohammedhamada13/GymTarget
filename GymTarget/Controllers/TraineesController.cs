using GymTarget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace GymTarget.Controllers
{
    public class TraineesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TraineesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            var trainees = _context.Trainees.ToList();

            return View(trainees);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Trainees trainees, IFormFile file)
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
					trainees.Image = @"Images\Products\" + filename + ext;
                }

                _context.Trainees.Add(trainees);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(trainees);

        }





        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _context.Trainees.Find(Id);

            if (item == null)
            {
                return NotFound();
            }


            return View(item);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Trainees trainees, IFormFile file)
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
					trainees.Image = @"Images\Products\" + filename + ext;
                }

                _context.Trainees.Update(trainees);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(trainees);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var trainees = _context.Trainees.Find(id);

            if (trainees == null)
            {
                return NotFound();
            }

            return View(trainees);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletecoaches(int? Id)

        {

            var trainees = _context.Trainees.Find(Id);
            if (trainees == null)
            {
                return NotFound();
            }
            _context.Remove(trainees);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }


    }
}
