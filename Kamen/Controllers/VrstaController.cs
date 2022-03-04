using Kamen.Data;
using Kamen.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Controllers
{
    public class VrstaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VrstaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Vrsta> objList = _db.Vrsta;
            return View(objList);
        }

        // GET - create 
        public IActionResult Create ()
        {
            return View();
        }

        // POST - create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vrsta obj) //object that we have to add to the database with
        {
            if(ModelState.IsValid)
            {
                _db.Vrsta.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);            
        }

        // GET - edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound(); 
            }
            var obj = _db.Vrsta.Find(id);
            if(obj==null)
            {
                return NotFound(); 
            }
            return View(obj);
        }

        // POST - edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vrsta obj) //object that we have to add to the database with
        {
            if (ModelState.IsValid)
            {
                _db.Vrsta.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET - delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Vrsta.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST - delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id) //object that we have to add to the database with
        {
            var obj = _db.Vrsta.Find(id);
            if(obj==null)
            {
                return NotFound();
            }
                _db.Vrsta.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }

    }
}
