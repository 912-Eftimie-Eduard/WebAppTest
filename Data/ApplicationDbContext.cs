using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAppTest.Models;

namespace WebAppTest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Persoana> Persoane { get; set; }
        public DbSet<Revista> Reviste { get; set; }
        public DbSet<Articol> Articole { get; set; }
    }
}
