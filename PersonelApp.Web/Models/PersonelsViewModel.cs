using System.ComponentModel.DataAnnotations;
using PersonelApp.Web.Entity;

namespace PersonelApp.Web.Models
{
    public class PersonelsViewModel
    {
        public List<PersonelViewModel> personels { get; set; }  
       
    }

    public class PersonelViewModel
    {
        public string Ad {  get; set; }
        public string Soyad { get; set; }
        public DateOnly Zaman { get; set; }
        public int GecenYıl { get; set; }
        public long KimlikNo { get; set; }
        public int BuYıl { get; set; }
        public int PersonelId { get; set; }
        public Fakulte Fakulte { get; set; }
        public Bolum Bolum { get; set; }
        public Abd Abd { get; set; }
    }

    public class FGYEditViewModel
    {
        public int PersonelId { get; set; }
        public int FakulteId { get; set; }
        public Fakulte Fakulte { get; set; }
        public int BolumId { get; set; }
        public Bolum Bolum { get; set; }
        public int AbdId { get; set; }
        public Abd Abd { get; set; }

    }
}
