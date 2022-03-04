using Kamen.Data;
using Kamen.Models;
using Kamen.Models.ViewModels;
using Kamen.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db; // HomeVM - 1
        
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {                                                           // HomeVM - 2                                    
            _logger = logger;
            _db = db;   // HomeVM - 3
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM() // HomeVM - 4 
            {
                Proizvodi = _db.Proizvod.Include(u => u.Vrsta).Include(u => u.TipApl),
                Vrste = _db.Vrsta
        };
            return View(homeVM); // HomeVM - 5
        }

        public IActionResult Details(int id)
        {
            List<ShopCart> shopCartsList = new List<ShopCart>();

            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart) != null
            && HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart).Count() > 0)
            { shopCartsList = HttpContext.Session.Get<List<ShopCart>>(WC.SessCart); }

            DetaljiVM DetaljiVM = new DetaljiVM()
            {
                Proizvod = _db.Proizvod.Include(u => u.Vrsta).Include(u => u.TipApl)
                .Where(u => u.Id == id).FirstOrDefault(),
                ImaGaKart = false
            };

            foreach (var item in shopCartsList)
            {
                if(item.ProizvodId==id)
                {
                    DetaljiVM.ImaGaKart = true;
                }    
            }

            return View(DetaljiVM);
        }


        // Dodavanje u korpu - POST
        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShopCart> shopCartsList = new List<ShopCart>();
           
            if(HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart)!=null
            && HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart).Count() > 0)
            { shopCartsList = HttpContext.Session.Get<List<ShopCart>>(WC.SessCart); }
            
            shopCartsList.Add(new ShopCart { ProizvodId = id });
            HttpContext.Session.Set(WC.SessCart, shopCartsList);

            return RedirectToAction(nameof(Index));
        }

        // UKLONITI IZ KARTA - ActionMethod
        public IActionResult UkloniIzKarta(int id)
        {
            List<ShopCart> shopCartsList = new List<ShopCart>();

            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart) != null
            && HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart).Count() > 0)
            { shopCartsList = HttpContext.Session.Get<List<ShopCart>>(WC.SessCart); }

            var itRmvFromCart = shopCartsList.SingleOrDefault(r => r.ProizvodId == id);
            if (itRmvFromCart!=null)
                { shopCartsList.Remove(itRmvFromCart); }

            HttpContext.Session.Set(WC.SessCart, shopCartsList);

            return RedirectToAction(nameof(Index));
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
