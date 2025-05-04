using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLojaBackofficeMvc.Data;
using AppLojaBackofficeMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLojaBackofficeMvc.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly ApplicationDbContext _context;
        public VendedorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vendedor?> BuscarPorIdAsync(string vendedorId)
        {
            return await _context.Vendedores
                .FirstOrDefaultAsync(v => v.VendedorId == vendedorId);
        }
        public async Task CriarAsync(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();

        }

    }
}