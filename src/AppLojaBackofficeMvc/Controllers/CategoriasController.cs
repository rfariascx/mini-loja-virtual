using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppLojaBackofficeMvc.Data;
using AppLojaBackofficeMvc.Models;
using Microsoft.AspNetCore.Authorization;
using AppLojaBackofficeMvc.Services;

namespace AppLojaBackofficeMvc.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;


        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;

        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            
            return View(await _categoriaService.ListarTodosAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaService.BuscarPorIdAsync(id.Value);
                
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaId,CategoriaDescricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaService.CriarAsync(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaService.BuscarPorIdAsync(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,CategoriaDescricao")] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoriaService.AtualizarAsync(categoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _categoriaService.BuscarPorIdAsync(categoria.CategoriaId) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaService.BuscarPorIdAsync(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _categoriaService.BuscarPorIdAsync(id);
            if (categoria == null)
            {
               return NotFound();

            }
                
            try
            {
                await _categoriaService.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["Erro"] = ex.Message;
                return RedirectToAction(nameof(Delete), new {id});
            }
            catch (Exception)
            {
                TempData["Erro"] = "Ocorreu um erro ao tentar excluir.";
                return RedirectToAction(nameof(Delete), new {id});
            }

            
            
        }
    }
}
