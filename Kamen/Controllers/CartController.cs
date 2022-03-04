using Kamen.Data;
using Kamen.Models;
using Kamen.Models.ViewModels;
using Kamen.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kamen.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // 0 - AppDbContext
        private ApplicationDbContext _db;
        
         // 5 - using PrzdKorVM
        [BindProperty] // now it doesn need to be defined as parameter
        public PrzdKorVM PrzdKorVM { get; set; }

        // 0.1.
        public CartController(ApplicationDbContext db) // db - constructor
        {
            _db = db;
        }

        //1 - to RETRIEVE all of the items 
        public IActionResult Index()
        {           
            //1.1. add a list for shoping cart
            List<ShopCart> shopCartList = new List<ShopCart>();

            //1.2. check session
            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart) != null
                && HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart).Count() > 0)
            {
                //session EXISTS and products can be displayed
                shopCartList = HttpContext.Session.Get<List<ShopCart>>(WC.SessCart);
                //IEnum to List
            }

            //1.3 find all distinct products in cart
            List<int> przdInCart = shopCartList.Select(i => i.ProizvodId).ToList();
                //another list wich has all productIDs
            IEnumerable<Proizvod> przdList = _db.Proizvod.Where(u => przdInCart.Contains(u.Id));

            return View(przdList);
        }

        // 3 - Index POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction(nameof(Sumarum)); // ime, emali, br. tel
        }

        // 4 - SUMARUM (info. o korisniku Ime, Prezime)
        public IActionResult Sumarum()
        {
            // 4.1. - podaci o KORISNIKU
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var korId = User.FindFirstValue(ClaimTypes.Name);


            // 4.2.-kupovna korpa--add a list for shoping cart & check session
            List<ShopCart> shopCartList = new List<ShopCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart) != null
                && HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart).Count() > 0)
            {
                //session EXISTS and products can be displayed
                shopCartList = HttpContext.Session.Get<List<ShopCart>>(WC.SessCart);
                //IEnum to List
            }

            List<int> przdInCart = shopCartList.Select(i => i.ProizvodId).ToList();
            //another list wich has all productIDs
            IEnumerable<Proizvod> przdList = _db.Proizvod.Where(u => przdInCart.Contains(u.Id));

            // 4.3.(after 5) 
            PrzdKorVM = new PrzdKorVM()
            {
                AppUser = _db.AppUser.FirstOrDefault(u => u.Id == claim.Value),
                PrzdLista = przdList
                
            };

            // 4.4.
            return View(PrzdKorVM);
        }

        //2 - REMOVE
        public IActionResult Remove(int id)
        {
            //2.1. add a list for shoping cart
            List<ShopCart> shopCartList = new List<ShopCart>();

            //2.2. check session
            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart) != null
                && HttpContext.Session.Get<IEnumerable<ShopCart>>(WC.SessCart).Count() > 0)
            {
                //2.3. session EXISTS and products can be displayed
                shopCartList = HttpContext.Session.Get<List<ShopCart>>(WC.SessCart);
                                                //IEnum to List
            }
                //2.4. remove Proizvod based on Id from list
            shopCartList.Remove(shopCartList.FirstOrDefault(u => u.ProizvodId == id));

            //2.5. set Session again, WC.SessCart is set with new shopCartList
            HttpContext.Session.Set(WC.SessCart, shopCartList); 

            //2.6. return index of ShopCart
            return RedirectToAction(nameof(Index));
        }
    }
}
