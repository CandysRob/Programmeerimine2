using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.DTO
{
    [ExcludeFromCodeCoverage]
    public class Projekt_DTO
    {

        public int Id { get; set; }

        public string Nimi { get; set; }

        public string Kirjeldus { get; set; }

        public DateTime Alguskuupaev { get; set; }

        public DateTime Lopetatuskuupaev { get; set; }

        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
