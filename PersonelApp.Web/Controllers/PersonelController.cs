using System.Runtime.CompilerServices;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PersonelApp.Web.Data;
using PersonelApp.Web.Entity;
using PersonelApp.Web.Models;

namespace PersonelApp.Web.Controllers
{
    
    public class PersonelController : Controller
    {
        private readonly PersonelContext _context;
        public PersonelController(PersonelContext context) 
        { 
            _context = context; 
        }
       

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(string q)
        {
            var personels = _context.Personels.AsQueryable();

            

            if (!string.IsNullOrEmpty(q))
            {
                personels = personels.Where(i =>
                i.Ad.ToLower().Contains(q.ToLower()) ||
                i.Soyad.ToLower().Contains(q.ToLower())
                );
            }


            var personel = new PersonelViewModel
            {

                Personels = personels.ToList()

            };

            return View(personel);
        }
        [HttpGet]
        public IActionResult Create(int? id) 
        {
            ViewBag.Fakultes= new SelectList(_context.Fakultes.ToList(),"FakulteId","FakulteName");
            ViewBag.Bolums = new SelectList(_context.Bolums.ToList(), "BolumId", "BolumName");
            ViewBag.Abds = new SelectList(_context.Abds.ToList(), "AbdId", "AbdName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Personel p)
        {
            if (ModelState.IsValid) 
            {
                bool KimlikKontrol= _context.Personels.Any(k => k.KimlikNo == p.KimlikNo);

                if (KimlikKontrol)
                {
                    ModelState.AddModelError("KimlikNo", "Bu TC Kimlik Numarası sistemde zaten kayıtlı.");
                    return View(p);
                }
                
                
                int diff1 = DateOnly.FromDateTime(DateTime.Today).Year-p.Zaman.Year;

                if (diff1 == 0)
                {
                    p.GecenYıl = 0;
                    p.BuYıl = 0;
                }
                if (diff1 >=1 && diff1 <=9)
                {
                    p.GecenYıl = 0;
                    p.BuYıl = 20;
                }
                if (diff1 >= 10)
                {
                    p.GecenYıl = 0;
                    p.BuYıl = 30;
                }
                _context.Personels.Add(p);
                _context.SaveChanges();
                TempData["Message"] = $"Yeni Kişi Eklendi.";
                return RedirectToAction("list");
            }
            return View();  
        }
        [HttpGet]
        public IActionResult IzinForm(int id)
        {
            var model = new IzinBilgisi
            {
                PersonelId = id,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult IzinForm(IzinBilgisi i)
        {
            
            if (ModelState.IsValid) 
            {

                int diff = (i.BitisTarihi - i.BaslangicTarihi).Days+1;
                i.Kullanilanizin=diff;
                _context.Izinler.Add(i);

                
            }

            var sonuc = _context.Personels.FirstOrDefault(p => p.PersonelId == i.PersonelId);

            if (sonuc != null) 
            {
                if(((sonuc.GecenYıl+sonuc.BuYıl) - i.Kullanilanizin) >= 0)
                {
                    sonuc.GecenYıl = sonuc.GecenYıl - i.Kullanilanizin;

                    if (sonuc.GecenYıl < 0)
                    {
                        int diff2;
                        diff2 = sonuc.GecenYıl * (-1);


                        sonuc.BuYıl = sonuc.BuYıl - diff2;
                        sonuc.GecenYıl = 0;

                    }

                    _context.SaveChanges();
                    TempData["Message"] = $"İzin Eklendi";
                    return RedirectToAction("Izinler");


                }

                if (((sonuc.GecenYıl + sonuc.BuYıl) - i.Kullanilanizin) < 0)
                {
                    ModelState.AddModelError("BitisTarihi", "Yıllık İzin Yetersizdir.");
                    return View(i);
                }



            }

            return View(i);
        }

        public IActionResult Izinler()
        {
            var izin = new IzinViewModel
            {

                Izinler = _context.Izinler.ToList()
            };

            return View(izin);
        }

        public JsonResult BolumToFakulte(int fakulteId) 
        {
            var bolums=_context.Bolums.Where(b =>b.FakulteList.Any(f=> f.FakulteId == fakulteId)).Select(b=> new {b.BolumId,b.BolumName}).ToList();
            return Json(bolums);
        }

        public JsonResult AbdToBolum(int bolumId)
        {
            var abds = _context.Abds
                .Where(a => a.BolumList.Any(b => b.BolumId == bolumId))
                .Select(a => new { a.AbdId, a.AbdName })
                .ToList();

            return Json(abds);
        }
        [HttpGet]
        public IActionResult Update()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Update(Personel p)
        {
            
            var personel = _context.Personels.ToList();


            foreach (var z in personel)
            {
                z.GecenYıl = z.BuYıl;
                z.BuYıl = 0;

                int diff1 = DateOnly.FromDateTime(DateTime.Today).Year - z.Zaman.Year;

                if (diff1 == 0)
                {
                    z.BuYıl = 0;
                }
                if (diff1 >= 1 && diff1 <= 9)
                {
                    z.BuYıl = 20;
                }
                if (diff1 >= 10)
                {
                    z.BuYıl = 30;
                }
                _context.SaveChanges();
            }
            
            

            return RedirectToAction("List");
        }

    }

}
