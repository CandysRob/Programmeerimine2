using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Projekt
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Nimi { get; set; }
        
        [MaxLength(500)]
        public string Kirjeldus { get; set; }
        
        [Required]
        public DateTime Alguskuupaev { get; set; }
        
        [Required]
        public DateTime Lopetatuskuupaev { get; set; }

        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
