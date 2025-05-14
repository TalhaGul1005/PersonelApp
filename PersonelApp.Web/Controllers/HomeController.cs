using Microsoft.AspNetCore.Mvc;
using PersonelApp.Web.Data;
using PersonelApp.Web.Entity;
using PersonelApp.Web.Models;

namespace PersonelApp.Web.Controllers
{
    public class HomeController(PersonelContext context) : Controller
    {
        private readonly PersonelContext _context = context;

        public IActionResult Index()
        {

            return View();
        }
    }
}
