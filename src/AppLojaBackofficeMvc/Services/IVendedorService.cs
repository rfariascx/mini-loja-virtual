using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLojaBackofficeMvc.Models;

namespace AppLojaBackofficeMvc.Services
{
    public interface IVendedorService
    {
        Task CriarAsync(Vendedor vendedor);
        Task<Vendedor?> BuscarPorIdAsync(string vendedorId);
    }
}