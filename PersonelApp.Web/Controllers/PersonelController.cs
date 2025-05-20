using System.Runtime.CompilerServices;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PersonelApp.Web.Data;
using PersonelApp.Web.Entity;
using PersonelApp.Web.Models;
using System.Linq;

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
        [HttpGet]
        public IActionResult List(string q)
        {
            var personel = _context.Personels.AsQueryable();


            ViewBag.Fakultes = new SelectList(_context.Fakultes.ToList(), "FakulteId", "FakulteName");
            ViewBag.Bolums = new SelectList(_context.Bolums.ToList(), "BolumId", "BolumName");
            ViewBag.Abds = new SelectList(_context.Abds.ToList(), "AbdId", "AbdName");


            if (!string.IsNullOrEmpty(q))
            {
                personel = personel.Where(i =>
                i.Ad.ToLower().Contains(q.ToLower()) ||
                i.Soyad.ToLower().Contains(q.ToLower())
                );
            }

            var view = new PersonelsViewModel
            {
                personels = personel
                .Include(p => p.Fakulte)
                .Include(p => p.Bolum)
                .Include(p => p.Abd)
                .Select(f => new PersonelViewModel
                {
                    PersonelId = f.PersonelId,
                    Ad = f.Ad,
                    Soyad = f.Soyad,
                    Zaman = f.Zaman,
                    GecenYıl = f.GecenYıl,
                    BuYıl = f.BuYıl,
                    Fakulte = f.Fakulte,
                    Bolum = f.Bolum,
                    Abd = f.Abd,
                    KimlikNo = f.KimlikNo
                })
                .ToList()
            };



            return View(view);
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
            
            //return View(p);  
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
            if (((i.BitisTarihi - i.BaslangicTarihi).Days + 1) < 0)
            {
                ModelState.AddModelError("BitisTarihi", "Bitiş tarihi başlangıçtan küçük olamaz.");
                return View(i);
            }
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

                Izinler = _context.Izinler.ToList(),
                Personels = _context.Personels.ToList(),
            };

            return View(izin);
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
        [HttpGet]
        public IActionResult Edit (int id)
        {
            
            return View(_context.Personels.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Personel p) 
        {
            var sonuc = _context.Personels.FirstOrDefault(i => i.PersonelId == p.PersonelId);

            if (sonuc != null) 
            {
                sonuc.Ad = p.Ad;
                sonuc.Soyad = p.Soyad;
                sonuc.GecenYıl=p.GecenYıl;
                sonuc.BuYıl = p.BuYıl;

                
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(p);
        }
        [HttpPost]
        public IActionResult Delete(int PersonelId) 
        { 
            var delete = _context.Personels.Find(PersonelId);
            _context.Personels.Remove(delete);
            _context.SaveChanges(); 
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult EditFGY(int id)
        {
            var entity = _context.Personels.Include(i => i.Fakulte).Include(i => i.Bolum).Include(i => i.Abd).Select(p=> new FGYEditViewModel
            {
                PersonelId = p.PersonelId,
                FakulteId = p.FakulteId,
                BolumId = p.BolumId,
                AbdId = p.AbdId,
                Fakulte = p.Fakulte,
                Bolum = p.Bolum,
                Abd = p.Abd
            }).FirstOrDefault(p=> p.PersonelId == id);

            ViewBag.Fakultes = new SelectList(_context.Fakultes.ToList(), "FakulteId", "FakulteName");
            ViewBag.Bolums = new SelectList(_context.Bolums.ToList(), "BolumId", "BolumName");
            ViewBag.Abds = new SelectList(_context.Abds.ToList(), "AbdId", "AbdName");
            return View(entity);
        }
        [HttpPost]
        public IActionResult EditFGY(Personel p)
        {
            var sonuc = _context.Personels.Find(p.PersonelId);

            if (sonuc != null)
            {
                sonuc.FakulteId = p.FakulteId;
                sonuc.BolumId = p.BolumId;
                sonuc.AbdId = p.AbdId;

                _context.SaveChanges();
                return RedirectToAction("List");
            }

            return View(p);
        }

        [HttpPost]
        public IActionResult IzinDelete(int IzinBilgisiId)
        {
            var delete = _context.Izinler.Find(IzinBilgisiId);

            var personel = _context.Personels.Find(delete.PersonelId);

            if (personel != null)
            {
                personel.BuYıl = personel.BuYıl+delete.Kullanilanizin;


            }

            _context.Izinler.Remove(delete);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }

}
