using AppLojaBackofficeMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppLojaBackofficeMvc.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
}
