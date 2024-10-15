using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    [Table("Revista")]
    [PrimaryKey(nameof(IdRevista))]
    public class Revista
    {
        public int IdRevista { get; set; }
        public string TipRevista { get; set; }
        public string TitluRevista { get; set; }
        public string Issn {  get; set; }
        public int AnAparitie { get; set; }
        public string Adresa { get; set; }
        public string Tara {  get; set; }
    }
}
