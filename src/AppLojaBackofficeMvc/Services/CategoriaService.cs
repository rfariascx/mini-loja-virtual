using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLojaBackofficeMvc.Data;
using AppLojaBackofficeMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLojaBackofficeMvc.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AtualizarAsync(Categoria categoria)
        {
           var categoriaExiste = await _context.Categorias.FindAsync(categoria.CategoriaId);
            if(categoriaExiste == null)
                throw new Exception("Categoria não encontrada");

            categoriaExiste.CategoriaId = categoria.CategoriaId;
            categoriaExiste.CategoriaDescricao = categoria.CategoriaDescricao;
            
            await _context.SaveChangesAsync();  
        }

        public async Task<Categoria?> BuscarPorIdAsync(int id)
        {
             return await _context.Categorias
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task CriarAsync(Categoria categoria)
        {
            
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Categorias.AnyAsync(c => c.CategoriaId == id);    
        }

        public async Task<List<Categoria>> ListarTodosAsync()
        {
            return await _context.Categorias
            .ToListAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var categoria  = await _context.Categorias.FindAsync(id);
            var categoriaEmUso = await _context.Produtos.AnyAsync(p => p.CategoriaId == id);
            
            if (categoriaEmUso == true)
            {
                throw new InvalidOperationException("Não é possível excluir a categoria. Existem produtos vinculados a ela.");

            }
            
                 
            if (categoria != null)
            {
                 _context.Categorias.Remove(categoria);
                 await _context.SaveChangesAsync();
            }
        }
    }
}