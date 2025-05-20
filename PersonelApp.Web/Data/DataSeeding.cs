using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonelApp.Web.Entity;

namespace PersonelApp.Web.Data
{
    public class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<PersonelContext>();

            context.Database.Migrate();

            var abds = new List<Abd>()
            {
                new Abd {AbdName="Abd1"},
                new Abd {AbdName="Abd2"},
                new Abd {AbdName="Abd3"},
                new Abd {AbdName="Abd4"},
                new Abd {AbdName="Abd5"},
                new Abd {AbdName="Abd6"},
                new Abd {AbdName="Abd7"},
                new Abd {AbdName="Abd8"},
                new Abd {AbdName="Abd9"},
                new Abd {AbdName="Abd10"}
            };

            var bolums = new List<Bolum>()
            {
                new Bolum {BolumName="Bölüm1", AbdList= new List<Abd> {abds[1],abds[2]} },
                new Bolum {BolumName="Bölüm2", AbdList= new List<Abd> {abds[3],abds[4],abds[5]} },
                new Bolum {BolumName="Bölüm3", AbdList= new List<Abd> {abds[6],abds[7],abds[8]} }
            };

            var fakultes = new List<Fakulte>()
            {
                new Fakulte{FakulteName="Fakülte1", BolumList= new List<Bolum>() {bolums[0],bolums[1],bolums[2] } }
            };

            var personels = new List<Personel>()
            {
                new Personel { KimlikNo = 12345678911, Ad = "Talha", Soyad = "Gül", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-1), GecenYıl = 10, BuYıl=0, Fakulte = fakultes[0], Bolum= bolums[1], Abd=abds[1] },
                new Personel { KimlikNo = 12345678912, Ad = "Rabia", Soyad = "Gül", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-3), GecenYıl = 20, BuYıl=10, Fakulte = fakultes[0], Bolum= bolums[1], Abd=abds[1] },
                new Personel { KimlikNo = 12345678913, Ad = "Mehmet", Soyad = "Kaya", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-10), GecenYıl = 30, BuYıl=20, Fakulte = fakultes[0], Bolum= bolums[1], Abd=abds[1] },
                new Personel {KimlikNo = 12345678914, Ad = "Ayşe", Soyad = "Yıldız", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-5), GecenYıl = 20, BuYıl = 30, Fakulte = fakultes[0], Bolum = bolums[1], Abd = abds[1]},
                new Personel {KimlikNo = 12345678915, Ad = "Ali", Soyad = "Şahin", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-11), GecenYıl = 30, BuYıl = 10, Fakulte = fakultes[0], Bolum = bolums[1], Abd = abds[1]},
                new Personel {KimlikNo = 12345678916, Ad = "Elif", Soyad = "Aydın", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-2), GecenYıl = 20, BuYıl = 20, Fakulte = fakultes[0], Bolum = bolums[1], Abd = abds[1]},
                new Personel {KimlikNo = 12345678917, Ad = "Can", Soyad = "Koç", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-6), GecenYıl = 20, BuYıl = 30, Fakulte = fakultes[0], Bolum = bolums[1], Abd = abds[1]},
                new Personel {KimlikNo = 12345678918, Ad = "Fatma", Soyad = "Arslan", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-12), GecenYıl = 30, BuYıl = 5, Fakulte = fakultes[0], Bolum = bolums[1], Abd = abds[1]},
                new Personel {KimlikNo = 12345678919, Ad = "Emre", Soyad = "Doğan", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-4), GecenYıl = 20, BuYıl = 10, Fakulte = fakultes[0], Bolum = bolums[1], Abd = abds[1]},
                new Personel {KimlikNo = 12345678920, Ad = "Seda", Soyad = "Bozkurt", Zaman = DateOnly.FromDateTime(DateTime.Today).AddYears(-9), GecenYıl = 20, BuYıl = 20, Fakulte = fakultes[0], Bolum = bolums[1], Abd = abds[1]}
            };


            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Abds.Count() == 0)
                {
                    context.Abds.AddRange(abds);
                }
                if (context.Bolums.Count() == 0)
                {
                    context.Bolums.AddRange(bolums);
                }
                if (context.Fakultes.Count() == 0)
                {
                    context.Fakultes.AddRange(fakultes);
                }
                if (context.Personels.Count() == 0)
                {
                    context.Personels.AddRange(personels);
                }
                

                context.SaveChanges();


            }
        }
    }
}
