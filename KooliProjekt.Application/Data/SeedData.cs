using System;
using System.Linq;

namespace KooliProjekt.Application.Data
{
    /// <summary>
    /// 14.11.2025
    /// Testandmete generaator
    /// Testandmed genereeritakse ainult siis kui mõni oluline tabel on tühi.
    /// </summary>
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;

        public SeedData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Genereerib andmed
        /// </summary>
        public void Generate()
        {
            if (_dbContext.Projektid.Any() || _dbContext.Tootajad.Any() || _dbContext.Ylesanded.Any() || _dbContext.Toologid.Any())
            {
                return;
            }

            // Projektid
            for (var i = 0; i < 10; i++)
            {
                var projekt = new Projekt
                {
                    Nimi = "Projekt " + (i + 1),
                    Kirjeldus = "Projekti kirjeldus " + (i + 1),
                    Alguskuupaev = DateTime.Today.AddDays(-30 + i),
                    Lopetatuskuupaev = DateTime.Today.AddDays(30 + i)
                };
                _dbContext.Projektid.Add(projekt);
            }

            _dbContext.SaveChanges();

            // Tootajad
            for (var i = 0; i < 10; i++)
            {
                var tootaja = new Tootaja
                {
                    Nimi = "Tootaja " + (i + 1),
                    Email = $"tootaja{i + 1}@firma.ee",
                    Ametikoht = "Arendaja"
                };
                _dbContext.Tootajad.Add(tootaja);
            }

            _dbContext.SaveChanges();

            var projektid = _dbContext.Projektid.ToList();
            var tootajad = _dbContext.Tootajad.ToList();

            // Ylesanded
            for (var i = 0; i < 10; i++)
            {
                var projekt = projektid[i % projektid.Count];
                var tootaja = tootajad[i % tootajad.Count];

                var ylesanne = new Ylesanne
                {
                    Pealkiri = "Ylesanne " + (i + 1),
                    Kirjeldus = "Ylesande kirjeldus " + (i + 1),
                    Tahtaeg = DateTime.Today.AddDays(7 + i),
                    Staatus = "Uus",
                    TunnidKokku = 8,
                    ProjektId = projekt.Id,
                    TootajaId = tootaja.Id
                };
                _dbContext.Ylesanded.Add(ylesanne);
            }

            // Toologid
            for (var i = 0; i < 10; i++)
            {
                var toologi = new toologi
                {
                    Nimi = "Toologi " + (i + 1),
                    starttime = 8,
                    endtime = 16,
                    Kirjeldus = "Toologi kirjeldus " + (i + 1)
                };
                _dbContext.Toologid.Add(toologi);
            }

            _dbContext.SaveChanges();
        }
    }
}
