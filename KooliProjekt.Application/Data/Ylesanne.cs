using System;

namespace KooliProjekt.Application.Data
{
    public class Ylesanne
    {
        public int Id { get; set; }
        public string Pealkiri { get; set; }
        public string Kirjeldus { get; set; }
        public DateTime Tahtaeg { get; set; }
        public string Staatus { get; set; }
        public decimal TunnidKokku { get; set; }

        public Projekt Projekt { get; set; }
        public int ProjektId { get; set; }

        public Tootaja Tootaja { get; set; }
        public int TootajaId { get; set; }
    }
}
