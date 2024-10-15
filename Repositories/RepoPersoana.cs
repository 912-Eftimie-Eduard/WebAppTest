using WebAppTest.Interfaces;
using WebAppTest.Data;
using WebAppTest.Models;

namespace WebAppTest.Repositories
{
    public class RepoPersoana : IRepoPersoana
    {
        private readonly ApplicationDbContext _context;

        public RepoPersoana(ApplicationDbContext context)
        {
            _context = context;
        }

        public Persoana GetPersoana(int id)
        {
            return _context.Persoane.Find(id);
        }

        public ICollection<Persoana> GetPersoane()
        {
            return _context.Persoane.ToList();
        }
    }
}
