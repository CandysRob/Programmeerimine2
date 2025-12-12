using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class toologi : Entity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Nimi { get; set; }

        public int starttime { get; set; }
		
        public int endtime { get; set; }

		[MaxLength(500)]
        public string Kirjeldus { get; set; }
    }
}
