using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    [Table("Persoana")]
    [PrimaryKey(nameof(IdPersoana))]
    public class Persoana
    {
        public int IdPersoana { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateOnly DataNasterii { get; set; }
    }
}
