using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.DTO
{
    public class Ylesanne_DTO
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
