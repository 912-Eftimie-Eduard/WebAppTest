using WebAppTest.Models;

namespace WebAppTest.Interfaces
{
    public interface IRepoArticol
    {
        public Articol GetById(int id);
        public ICollection<Articol> GetArticols();
        public bool Add(Articol articol);
        public bool Update(Articol articol);
        public bool Delete(int id);
        public ICollection<Articol> GetArticlesOfUser(int userId);
    }
}
