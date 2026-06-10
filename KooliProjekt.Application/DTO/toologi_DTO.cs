using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.DTO
{
    [ExcludeFromCodeCoverage]
    public class toologi_DTO
    {
        public int Id { get; set; }
        public string Nimi { get; set; }

        public int starttime { get; set; }

        public int endtime { get; set; }
        public string Kirjeldus { get; set; }
    }
}
