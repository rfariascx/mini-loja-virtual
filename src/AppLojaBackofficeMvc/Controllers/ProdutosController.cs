using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppLojaBackofficeMvc.Data;
using AppLojaBackofficeMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AppLojaBackofficeMvc.Services;


namespace AppLojaBackofficeMvc.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;

        
        public ProdutosController(
        UserManager<IdentityUser> userManager,
        IProdutoService produtoService,
        ICategoriaService categoriaService)
        {
         _userManager = userManager;
         _produtoService = produtoService;
         _categoriaService = categoriaService;
        }
        
        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var vendedorLogado = _userManager.GetUserId(User);
            var produtos = await _produtoService.ListarTodosAsync();
            var produtosDoVendedor = produtos
                .Where(p => p.VendedorId == vendedorLogado)
                .ToList();
            return View(produtosDoVendedor);
          
           
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vendedorLogado = _userManager.GetUserId(User);
            var produto = await _produtoService.BuscarPorIdAsync(id.Value);
                
            if (produto == null || vendedorLogado != produto.VendedorId)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaService.ListarTodosAsync();
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "CategoriaDescricao");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Produto produto)
        {
            // Adiciona o vendedor logado
            var userId = _userManager.GetUserId(User);
            produto.VendedorId = userId;
            

            ModelState.Remove("VendedorId");
            ModelState.Remove("Categoria");
            
           if (ModelState.IsValid)
           {                      
                await _produtoService.CriarAsync(produto);
                return RedirectToAction(nameof(Index));
           }
            var categorias = await _categoriaService.ListarTodosAsync();
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "CategoriaDescricao", produto.CategoriaId);
            return View(produto);

           
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vendedorLogado = _userManager.GetUserId(User);
            var produto = await _produtoService.BuscarPorIdAsync(id.Value);
            if (produto == null || vendedorLogado != produto.VendedorId)
            {
                return NotFound();
            }

            var categorias = await _categoriaService.ListarTodosAsync();
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "CategoriaDescricao", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,ProdutoDescricao,ProdutoImagem,ProdutoPreco,ProdutoEstoque,CategoriaId,VendedorId")] Produto produto)
        {
            if (await _produtoService.BuscarPorIdAsync(produto.ProdutoId) == null)
            {
                return NotFound();
            }
            var vendedorLogado = _userManager.GetUserId(User);
            if (produto.VendedorId != vendedorLogado)
            {
                TempData["Erro"] = "Você não tem permissão para editar este produto.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.Remove("VendedorId");
            ModelState.Remove("Categoria");
           
            if (ModelState.IsValid)
            {
                try
                {
                   await _produtoService.AtualizarAsync(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                 throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var categorias = await _categoriaService.ListarTodosAsync();
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "CategoriaDescricao", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                      
            var vendedorLogado = _userManager.GetUserId(User);
            var produto = await _produtoService.BuscarPorIdAsync(id.Value);
            if (produto == null || vendedorLogado != produto.VendedorId)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedorLogado = _userManager.GetUserId(User);
            var produto = await _produtoService.BuscarPorIdAsync(id);
            if (produto != null && vendedorLogado == produto.VendedorId)
            {
                await _produtoService.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Erro"] = "Você não tem permissão para deletar este produto.";
                return RedirectToAction(nameof(Index));
            }
                       
        }
        
    }
}
