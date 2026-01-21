using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Tootaja
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Nimi { get; set; }
        
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Ametikoht { get; set; }

        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
