using WebAppTest.Models;

namespace WebAppTest.Interfaces
{
    public interface IRepoRevista
    {
        public Revista GetRevista(int id);
        public ICollection<Revista> GetReviste();
        public bool AddRevista(Revista revista);
    }
}
