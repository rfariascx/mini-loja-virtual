using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLojaBackofficeMvc.Models;

namespace AppLojaBackofficeMvc.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> ListarTodosAsync();
        Task<Produto?> BuscarPorIdAsync(int id);

        Task CriarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(int id);
        
    }
}