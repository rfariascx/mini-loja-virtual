using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLojaBackofficeMvc.Services
{
    public interface IProdutoImagemService
    {
        Task<string?> SalvarImagemAsync(IFormFile imagem);
    }
}