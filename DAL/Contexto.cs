using Ap1_P1_JairoCamilo.Models;
using Microsoft.EntityFrameworkCore;

namespace Ap1_P1_JairoCamilo.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        
    }

    public DbSet<Articulos> Articulos { get; set; }
}
