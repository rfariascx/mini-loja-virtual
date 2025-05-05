using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLojaBackofficeMvc.Services
{
    public class ProdutoImagemService : IProdutoImagemService
    {
        private readonly string _caminhoPasta;

        public ProdutoImagemService(IConfiguration config)
        {
            // caminho /AppLojaBackofficeMvc/wwwroot/images/produtos
            _caminhoPasta = config["ConfiguracoesUpload:CaminhoImagens"] 
                ?? throw new Exception("Caminho de imagens não configurado.");
        }

        public async Task<string?> SalvarImagemAsync(IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
                return null;

            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);

            if (!Directory.Exists(_caminhoPasta))
                Directory.CreateDirectory(_caminhoPasta);

            var caminhoCompleto = Path.Combine(_caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            return Path.Combine("images", "produtos", nomeArquivo).Replace("\\", "/");

        }
    }
}
