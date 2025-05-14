using System.ComponentModel.DataAnnotations;

namespace PersonelApp.Web.Entity
{
    public class IzinBilgisi
    {
        public int IzinBilgisiId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public int Kullanilanizin { get; set; }
        [Required]
        public int PersonelId { get; set; }

    }
}
