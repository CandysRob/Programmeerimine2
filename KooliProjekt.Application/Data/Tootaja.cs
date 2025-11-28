using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Tootaja
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Email { get; set; }
        public string Ametikoht { get; set; }

        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
