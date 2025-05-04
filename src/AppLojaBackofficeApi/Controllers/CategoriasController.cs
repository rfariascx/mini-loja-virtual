using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLojaBackofficeMvc.Models;
using AppLojaBackofficeMvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLojaBackofficeApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: api/categorias
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaService.ListarTodosAsync();
            return Ok(categorias);
        }

        // GET: api/categorias/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _categoriaService.BuscarPorIdAsync(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        // POST: api/categorias
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            categoria.CategoriaId = 0;
            ModelState.Remove("CategoriaId");
            
                     
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoriaService.CriarAsync(categoria);
            return CreatedAtAction(nameof(GetById), new { id = categoria.CategoriaId }, categoria);
        }

        // PATCH: api/categorias/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest("ID da categoria não confere.");

            var categoriaExistente = await _categoriaService.BuscarPorIdAsync(id);
            if (categoriaExistente == null)
                return NotFound();

            await _categoriaService.AtualizarAsync(categoria);
            return NoContent();
        }

        // DELETE: api/categorias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.BuscarPorIdAsync(id);
            if (categoria == null)
                return NotFound();

            await _categoriaService.RemoverAsync(id);
            return NoContent();
        }
    }
}