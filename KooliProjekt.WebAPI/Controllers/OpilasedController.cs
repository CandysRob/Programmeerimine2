using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.WebAPI.Controllers
{
    public class OpilasedController
    {
        using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
    {
        public class Opilased
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [MaxLength(100)]
            public string Eesnimi { get; set; }

            [Required]
            [MaxLength(100)]
            public string PerENimi { get; set; }

            public int Vanus { get; set; }

            public string Klass { get; set; }
        }
    }

}
}
