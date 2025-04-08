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


namespace AppLojaBackofficeMvc.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        
        public ProdutosController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
        {
         _context = context;
         _userManager = userManager;
        }
        
        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var vendedorLogado = _userManager.GetUserId(User);
            var applicationDbContext = _context.Produtos
            .Include(p => p.Categoria)
            .Where(p=>p.VendedorId == vendedorLogado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vendedorLogado = _userManager.GetUserId(User);
            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p=>p.VendedorId == vendedorLogado)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
                
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaDescricao");
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
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaDescricao", produto.CategoriaId);
            return View(produto);

           
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaDescricao", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,ProdutoDescricao,ProdutoImagem,ProdutoPreco,ProdutoEstoque,CategoriaId,VendedorId")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            ModelState.Remove("VendedorId");
            ModelState.Remove("Categoria");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaDescricao", produto.CategoriaId);
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
            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p=>p.VendedorId == vendedorLogado)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
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
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null && vendedorLogado == produto.VendedorId)
            {
                _context.Produtos.Remove(produto);
            }
            else
            {
                TempData["Erro"] = "Você não tem permissão para deletar este produto.";
                return RedirectToAction(nameof(Index));
            }
             

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
