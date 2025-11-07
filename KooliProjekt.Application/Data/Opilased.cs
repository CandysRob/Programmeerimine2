using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Opilased
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Eesnimi { get; set; }

        [Required, MaxLength(100)]
        public string Perenimi { get; set; }

        public int Vanus { get; set; }

        [MaxLength(50)]
        public string Klass { get; set; }
    }
}
