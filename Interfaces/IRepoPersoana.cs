using WebAppTest.Models;

namespace WebAppTest.Interfaces
{
    public interface IRepoPersoana
    {
        public Persoana GetPersoana(int id);
        public ICollection<Persoana> GetPersoane();
    }
}
