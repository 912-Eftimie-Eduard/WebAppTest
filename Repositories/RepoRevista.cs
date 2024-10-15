using WebAppTest.Interfaces;
using WebAppTest.Data;
using WebAppTest.Models;

namespace WebAppTest.Repositories
{
    public class RepoRevista : IRepoRevista
    {
        private readonly ApplicationDbContext _context;
        public RepoRevista(ApplicationDbContext context)
        {
            _context = context;
        }
        public Revista GetRevista(int id)
        {
            return _context.Reviste.Find(id);
        }
        public ICollection<Revista> GetReviste()
        {
            return _context.Reviste.ToList();
        }
        public bool AddRevista(Revista revista)
        {
            try
            {
                _context.Reviste.Add(revista);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
