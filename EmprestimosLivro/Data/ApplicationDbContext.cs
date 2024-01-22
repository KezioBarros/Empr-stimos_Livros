using EmprestimosLivro.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimosLivro.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }

        public DbSet<EmprestimosModels> Emprestimos { get; set;}
    }
}
