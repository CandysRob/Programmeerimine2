using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.WpfApplication
{
    public class toologi
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public int starttime { get; set; }
        public int endtime { get; set; }
        public string Kirjeldus { get; set; }
    }
}
