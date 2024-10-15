using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    [Table("Articol")]
    [PrimaryKey(nameof(IdArticol))]
    public class Articol
    {
        public int IdArticol { get; set; }
        public int IdAutorPrincipal {  get; set; }
        public int IdRevista {  get; set; }
        public string TitluArticol { get; set; }
        public int AnPublicare { get; set; }
        public int NrPagini { get; set; }
        public int NrCoautori { get; set; }
    }
}
