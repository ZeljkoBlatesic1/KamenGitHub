using Kamen.Data;
using Kamen.Models;
using Kamen.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Kamen.Controllers
{
    public class ProizvodController : Controller
    {
        
        // _db
        private readonly ApplicationDbContext _db;

        // _webHostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // _db=db , _webHostEnvironment = webHostEnvironment
        public ProizvodController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // INDEX
        public IActionResult Index()
        {
            IEnumerable<Proizvod> objList = _db.Proizvod.Include(u=>u.Vrsta).Include(u=>u.TipApl);

            //// PRVI NACIN - Vrsta is used like object and VrstaId is FK
            //foreach (var obj in objList)
            //{
            //    obj.Vrsta = _db.Vrsta.FirstOrDefault(u => u.Id == obj.VrstaId);
            //    obj.TipApl = _db.TipApl.FirstOrDefault(u => u.Id == obj.TipAplId);
            //}

            return View(objList);
        }

        // UPSERT - get
        public IActionResult Upsert (int? id)
        {
            //IEnumerable<SelectListItem> VrstaDropDown = _db.Vrsta.Select(i => new SelectListItem
            //{
            //    Text = i.Naziv,
            //    Value = i.Id.ToString()
            //}) ;

            //ViewBag.VrstaDropDown = VrstaDropDown;

            //Proizvod proizvod = new Proizvod();

            ProizvodVM proizvodVM = new ProizvodVM()
            {
                Proizvod = new Proizvod(),
                VrstaSelectList = _db.Vrsta.Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString()
                }),
                TipAplSelectList = _db.TipApl.Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString()
                })
            };


            if (id==null) //than CREATE works
            {
                return View(proizvodVM);
            }

            else 
            {
                proizvodVM.Proizvod = _db.Proizvod.Find(id);
                if (proizvodVM.Proizvod==null)
                {
                    return NotFound();
                }
                return View(proizvodVM);
            }            
        }

        // UPSERT - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProizvodVM proizvodVM) //object that we have to add to the database with
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (proizvodVM.Proizvod.Id == 0)
                {
                    // Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    proizvodVM.Proizvod.Slika = fileName + extension;

                    _db.Proizvod.Add(proizvodVM.Proizvod); 
                }
                else
                {
                    // Updating
                    var objFromDb = _db.Proizvod.AsNoTracking().FirstOrDefault(u => u.Id == proizvodVM.Proizvod.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Slika);

                        if(System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        
                        
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        proizvodVM.Proizvod.Slika = fileName + extension;
                    }
                    else
                    {
                        proizvodVM.Proizvod.Slika = objFromDb.Slika;
                    }
                    _db.Proizvod.Update(proizvodVM.Proizvod);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            proizvodVM.VrstaSelectList = _db.Vrsta.Select(i => new SelectListItem
            {
                Text = i.Naziv,
                Value = i.Id.ToString()

            });

            proizvodVM.TipAplSelectList = _db.TipApl.Select(i => new SelectListItem
            {
                Text = i.Naziv,
                Value = i.Id.ToString()

            });

            return View(proizvodVM);            
        }

        //DELETE - get           
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            Proizvod proizvod =  _db.Proizvod.Include(u=>u.Vrsta).Include(u=>u.TipApl).FirstOrDefault(u=>u.Id == id);
            //proizvod.Vrsta = _db.Vrsta .Find(proizvod.VrstaId); 

            if (proizvod == null)
            {
                return NotFound();
            }
            return View(proizvod);
        }

        // DELETE - post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id) //object that we have to add to the database with
        {
            var obj = _db.Proizvod.Find(id);
            if(obj==null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;

            var oldFile = Path.Combine(upload, obj.Slika);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Proizvod.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }


        //// EDIT - get&post
        
        //// EDIT - get
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id==0)
        //    {
        //        return NotFound(); 
        //    }
        //    var obj = _db.Proizvod.Find(id);
        //    if(obj==null)
        //    {
        //        return NotFound(); 
        //    }
        //    return View(obj);
        //}

        //// EDIT - post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Proizvod obj) //object that we have to add to the database with
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Proizvod.Update(obj);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}

    }
}
