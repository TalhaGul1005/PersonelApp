using System.ComponentModel.DataAnnotations;

namespace PersonelApp.Web.Entity
{
    public class Personel
    {
        public int PersonelId { get; set; }
        [Required]
        [Range(10000000000,99999999999)]
        public long KimlikNo { get; set; }
        
        public string? Ad { get; set; }
        
        public string? Soyad { get; set; }
        public DateOnly Zaman { get; set; }
        
        public int GecenYıl { get; set; }
        public int BuYıl { get; set; }
        public int FakulteId { get; set; }
        public int BolumId { get; set; }
        public int AbdId { get; set; }


    }
}
