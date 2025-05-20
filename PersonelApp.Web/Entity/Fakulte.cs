using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PersonelApp.Web.Entity
{
    public class Fakulte
    {
        public int FakulteId { get; set; }
        public string FakulteName { get; set; }
        public List<Bolum> BolumList { get; set; }
    }
}