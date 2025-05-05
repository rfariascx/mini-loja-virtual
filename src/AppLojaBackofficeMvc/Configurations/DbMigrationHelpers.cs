using AppLojaBackofficeMvc.Data;
using AppLojaBackofficeMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppLojaBackofficeApi.Configurations
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await EnsureSeedEntities(scope);
            }
        }

        private static async Task EnsureSeedEntities(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Seed Categoria
            if (!context.Categorias.Any())
            {
                context.Categorias.AddRange(
                    new Categoria { CategoriaDescricao = "Pneu de Passeio" },
                    new Categoria { CategoriaDescricao = "Pneu de Caminhão" },
                    new Categoria { CategoriaDescricao = "Pneu de Moto"}
                );
                await context.SaveChangesAsync();
            }

            // Seed Usuário + Vendedor
            if (!userManager.Users.Any())
            {
                var user = new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "vendedordeteste@teste.com",
                    Email = "vendedordeteste@teste.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Senha@2025");

                if (result.Succeeded)
                {
                    var vendedor = new Vendedor
                    {
                        VendedorId = user.Id,
                        VendedorNomeCompleto = "Vendedor de Teste",
                        VendedorEmail = user.Email
                    };
                    context.Vendedores.Add(vendedor);
                    await context.SaveChangesAsync();
                }
            }

            // Seed Produtos
            if (!context.Produtos.Any())
            {
                var categoria = context.Categorias.First();
                var vendedorId = context.Vendedores.First().VendedorId;

                context.Produtos.AddRange(
                    new Produto
                    {
                        ProdutoDescricao = "Pneu Highlander 175/65R14 88T",
                        ProdutoPreco = 399,
                        ProdutoEstoque = 10,
                        ProdutoImagem = "images/produtos/1e56f006-75a1-47b9-852f-de6e0a067f98.jpeg",
                        CategoriaId = categoria.CategoriaId,
                        VendedorId = vendedorId
                    },
                    new Produto
                    {
                        ProdutoDescricao = "Pneu Charm 185/65R14 88H",
                        ProdutoPreco = 499,
                        ProdutoEstoque = 14,
                        ProdutoImagem = "images/produtos/1e56f006-75a1-47b9-852f-de6e0a067f98.jpeg",
                        CategoriaId = categoria.CategoriaId,
                        VendedorId = vendedorId
                    },
                    new Produto
                    {
                        ProdutoDescricao = "Pneu Deoxys 195/65R15 95V",
                        ProdutoPreco = 599,
                        ProdutoEstoque = 8,
                        ProdutoImagem = "images/produtos/1e56f006-75a1-47b9-852f-de6e0a067f98.jpeg",
                        CategoriaId = categoria.CategoriaId,
                        VendedorId = vendedorId
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
