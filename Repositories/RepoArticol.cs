using WebAppTest.Models;
using WebAppTest.Data;
using WebAppTest.Interfaces;

namespace WebAppTest.Repositories
{
    public class RepoArticol
    {
        private readonly ApplicationDbContext _context;
        public RepoArticol (ApplicationDbContext context)
        {
            _context = context;
        }
        public Articol GetById(int id)
        {
            return _context.Articole.Find(id);
        }
        public ICollection<Articol> GetArticols()
        {
            return _context.Articole.ToList();
        }
        public bool Add(Articol articol)
        {
            try
            {
                _context.Articole.Add(articol);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            var articol = _context.Articole.Find(id);
            if (articol != null)
            {
                _context.Articole.Remove(articol);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Articol articol)
        {
            try
            {
                _context.Articole.Update(articol);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public ICollection<Articol> GetArticlesOfUser(int userId)
        {

            ICollection<Articol> articolsByUser = _context.Articole.Where(a => a.IdAutorPrincipal == userId).ToList();
            return articolsByUser;
        }
    }
}
