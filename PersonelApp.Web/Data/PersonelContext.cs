using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using PersonelApp.Web.Entity;


namespace PersonelApp.Web.Data
{
    public class PersonelContext : DbContext
    {
        public PersonelContext(DbContextOptions<PersonelContext> options) : base(options)
        {
        }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<IzinBilgisi> Izinler { get; set; }
        public DbSet<Fakulte> Fakultes { get; set; }
        public DbSet<Bolum> Bolums { get; set; }
        public DbSet<Abd> Abds { get; set; }    

    }
}
