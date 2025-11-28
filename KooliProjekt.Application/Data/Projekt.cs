using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Projekt
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Kirjeldus { get; set; }
        public DateTime Alguskuupaev { get; set; }
        public DateTime Lopetatuskuupaev { get; set; }

        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
