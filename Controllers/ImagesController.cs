using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestImages.Data;
using TestImages.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestImages.Controllers
{
    public class ImagesController : Controller
    {
        private readonly TestImagesDbContext _context;

        public ImagesController(TestImagesDbContext context)
        {
            _context = context;
        }
        // GET: ImagesController
        public ActionResult Index()
        {
           
            return View(_context.Image.ToList());
        }

        // GET: ImagesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Image.FirstOrDefault(d=>d.Id==id));
        }

        // GET: ImagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Images images)
        {
            try
            {
                //هنايتم اخذ الصورة
                var file = HttpContext.Request.Form.Files;
                //يتم الفحص اذا وجد صورة
                if (file.Count() > 0)
                {
                    //هنا يتم اخذ اسم الصورة مع الامتداد ويتم اضافة رقم عشوائي للاسم عشان تكرر الصور

                    //دالة لتوليد رقم لا يحتوي على علامة (.)
                    //Guid.NewGuid() يولد رقم عشوائي

                    Guid newGuid = RemoveDotFromGuid(Guid.NewGuid());

                    string ImageName = newGuid.ToString() + Path.GetExtension(file[0].FileName);
                   
                    //هنا يتم قرائة امتداد الملف الذي بنخزن فية الصورة
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/", ImageName), FileMode.Create);
                   
                    //هنا نسخنا الصورة للملف 
                    file[0].CopyTo(fileStream);
                  
                    //هنا بنسند الاسم الجديد الذي كوناه طالع
                    images.ImageUrl = ImageName;
                }
               
                _context.Image.Add(images);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ImagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ImagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ImagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public static Guid RemoveDotFromGuid(Guid guid)
        {
            string guidString = guid.ToString();
            string newGuidString = guidString.Replace(".", "");
            Guid newGuid = new Guid(newGuidString);
            return newGuid;
        }
    }
}
