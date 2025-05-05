using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppLojaBackofficeMvc.Models;
using AppLojaBackofficeMvc.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppLojaBackofficeApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;

        private readonly IProdutoImagemService _produtoImagemService;

        public ProdutosController(IProdutoService produtoService, 
        ICategoriaService categoriaService, 
        IProdutoImagemService produtoImagemService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _produtoImagemService = produtoImagemService;
        }

        //pegar o vendedor logado por meio do token JWT
        private Guid VendedorLogadoJwt()
        {
            var claim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value 
             ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(claim))
                throw new Exception("Token JWT inválido: claim de identificação não encontrada.");

            if (!Guid.TryParse(claim, out var guid))
            throw new Exception($"Token JWT inválido: claim ('{claim}') não é um GUID válido.");

    return guid;
}
        
        //GET: /api/produtos
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
           var produtos = await _produtoService.ListarTodosAsync();
           return Ok(produtos);
        }

        //GET: /api/produto/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.BuscarPorIdAsync(id);
            if (produto == null)
            {
                return NotFound();

            }
            else
            {
            return Ok(produto);
            }
        }

        //GET: /api/produto/por-categoria/{id}
        [HttpGet("por-categoria/{categoriaId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPorCategoria(int categoriaId)
        {
            var produtos = await _produtoService.ObterTodosPorCategoriaAsync(categoriaId);
            return Ok(produtos);
        }


        // POST: /api/produto
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] Produto produto)
        {
            
                      
            var vendedorLogado = VendedorLogadoJwt().ToString();
            produto.VendedorId = vendedorLogado;

            produto.ProdutoId = 0;
            produto.Categoria = null;

            ModelState.Remove("VendedorId");
            ModelState.Remove("Categoria");
            ModelState.Remove("ProdutoId");

             if (!await _categoriaService.ExisteAsync(produto.CategoriaId))
             {
                    return BadRequest("A categoria informada não existe.");
             }

            
            if (ModelState.IsValid)
            {
                var caminhoImagem = await _produtoImagemService.SalvarImagemAsync(produto.ImagemUpload);
                produto.ProdutoImagem = caminhoImagem;

                await _produtoService.CriarAsync(produto);
                return CreatedAtAction(nameof(GetById), new { id = produto.ProdutoId }, produto);
            }
            else
            { 
                return BadRequest(ModelState);
            }            
        }

        //PATCH: /api/produto/{id}
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(int id, [FromForm] Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest("Produto não encontrado");

            var vendedorLogado = VendedorLogadoJwt().ToString();
            var produtoExistente = await _produtoService.BuscarPorIdAsync(id);
            

            if (produtoExistente == null)
            {
                return NotFound();
            }
                

            if (produtoExistente.VendedorId != vendedorLogado)
            {
                return Forbid("Você não tem permissão para editar este produto.");
            }    

              if (produto.ImagemUpload != null)
            {
                var caminhoImagem = await _produtoImagemService.SalvarImagemAsync(produto.ImagemUpload);
                produto.ProdutoImagem = caminhoImagem;
            }
            
            produto.VendedorId = produtoExistente.VendedorId;
            produto.Categoria = null;

            ModelState.Remove("ProdutoId");
            ModelState.Remove("VendedorId");
            ModelState.Remove("Categoria");        

            if (!await _categoriaService.ExisteAsync(produto.CategoriaId))
            {
            return BadRequest("A categoria informada não existe.");
            }

            if (ModelState.IsValid)
            {  

                await _produtoService.AtualizarAsync(produto);
                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }         
        }

        // DELETE: /api/produto/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var vendedorLogado = VendedorLogadoJwt().ToString();
            var produto = await _produtoService.BuscarPorIdAsync(id);

            if (produto == null)
                return NotFound();

            if (produto.VendedorId != vendedorLogado)
                return Forbid("Você não tem permissão para excluir este produto.");

            await _produtoService.RemoverAsync(id);
            return NoContent();
        }
    }
}