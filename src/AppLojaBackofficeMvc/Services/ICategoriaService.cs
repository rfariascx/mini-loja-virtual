using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLojaBackofficeMvc.Models;

namespace AppLojaBackofficeMvc.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ListarTodosAsync();
        Task<Categoria?> BuscarPorIdAsync(int id);
        Task CriarAsync(Categoria categoria);
        Task AtualizarAsync(Categoria categoria);
        Task RemoverAsync(int id);
        Task<bool> ExisteAsync(int categoriaId);

        
    }
}