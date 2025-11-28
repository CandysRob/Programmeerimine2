using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KooliProjekt.Application.Data
{
    public class Ylesanne
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string Pealkiri { get; set; }
        
        [MaxLength(1000)]
        public string Kirjeldus { get; set; }
        
        [Required]
        public DateTime Tahtaeg { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Staatus { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TunnidKokku { get; set; }

        public Projekt Projekt { get; set; }
        [Required]
        public int ProjektId { get; set; }

        public Tootaja Tootaja { get; set; }
        [Required]
        public int TootajaId { get; set; }
    }
}
