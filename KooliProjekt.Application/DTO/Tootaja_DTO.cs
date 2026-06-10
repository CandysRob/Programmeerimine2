using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.DTO
{
    [ExcludeFromCodeCoverage]
    public class Tootaja_DTO
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Email { get; set; }
        public string Ametikoht { get; set; }
        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
