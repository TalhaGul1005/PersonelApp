namespace PersonelApp.Web.Entity
{
    public class Bolum
    {
        public int BolumId { get; set; }
        public string BolumName { get; set; }
        public List<Fakulte> FakulteList { get; set; }
        public List<Abd> AbdList { get; set; }
    }
}
