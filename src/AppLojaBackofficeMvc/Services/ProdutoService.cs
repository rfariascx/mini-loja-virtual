using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLojaBackofficeMvc.Data;
using AppLojaBackofficeMvc.Models;
using Microsoft.EntityFrameworkCore;    
using AppLojaBackofficeMvc.Services;

namespace AppLojaBackofficeMvc.Services
{
    public class ProdutoService : IProdutoService
    {
        
        private readonly ApplicationDbContext _context;
        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ListarTodosAsync()
        {
            return await _context.Produtos
            .Include(p => p.Categoria)
            .ToListAsync();
            
        }

        public async Task<Produto?> BuscarPorIdAsync(int id)
        {
           
            return await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
        }

        public async Task AtualizarAsync(Produto produto)
        {
            var produtoExiste = await _context.Produtos.FindAsync(produto.ProdutoId);
            if(produtoExiste == null)
                throw new Exception("Produto não encontrado");

            produtoExiste.ProdutoDescricao = produto.ProdutoDescricao;
            produtoExiste.ProdutoImagem = produto.ProdutoImagem;
            produtoExiste.ProdutoPreco = produto.ProdutoPreco;
            produtoExiste.ProdutoEstoque = produto.ProdutoEstoque;
            produtoExiste.CategoriaId = produto.CategoriaId;

            await _context.SaveChangesAsync();
        }

        

        public async Task CriarAsync(Produto produto)
        {
            
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

       
        public async Task RemoverAsync(int id)
        {
            var produto  = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                 _context.Produtos.Remove(produto);
                 await _context.SaveChangesAsync();
            }
        }
    }
}