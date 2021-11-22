using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarWash.WebApp.MVC.Models;

namespace CarWash.WebApp.MVC.Controllers
{
    public class LavagensController : Controller
    {
        private readonly carwashContext _context;

        public LavagensController(carwashContext context)
        {
            _context = context;
        }

        // GET: Lavagens
        public async Task<IActionResult> Index()
        {
            var carwashContext = _context.Lavagens.Include(l => l.CodTipoLavagemNavigation).Include(l => l.CpfNavigation).Include(l => l.PlacaNavigation);
            return View(await carwashContext.ToListAsync());
        }

        // GET: Lavagens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavagen = await _context.Lavagens
                .Include(l => l.CodTipoLavagemNavigation)
                .Include(l => l.CpfNavigation)
                .Include(l => l.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.Cpf == id);
            if (lavagen == null)
            {
                return NotFound();
            }

            return View(lavagen);
        }

        // GET: Lavagens/Create
        public IActionResult Create()
        {
            ViewData["CodTipoLavagem"] = new SelectList(_context.TipoLavagems, "Codigo", "NomeLavagem");
            ViewData["Cpf"] = new SelectList(_context.Funcionarios, "Cpf", "Cpf");
            ViewData["Placa"] = new SelectList(_context.Veiculos, "Placa", "Placa");
            return View();
        }

        // POST: Lavagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,Placa,DataLavagem,CodTipoLavagem,Valor")] Lavagen lavagen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lavagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodTipoLavagem"] = new SelectList(_context.TipoLavagems, "Codigo", "NomeLavagem", lavagen.CodTipoLavagem);
            ViewData["Cpf"] = new SelectList(_context.Funcionarios, "Cpf", "Cpf", lavagen.Cpf);
            ViewData["Placa"] = new SelectList(_context.Veiculos, "Placa", "Placa", lavagen.Placa);
            return View(lavagen);
        }

        // GET: Lavagens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavagen = await _context.Lavagens.FindAsync(id);
            if (lavagen == null)
            {
                return NotFound();
            }
            ViewData["CodTipoLavagem"] = new SelectList(_context.TipoLavagems, "Codigo", "NomeLavagem", lavagen.CodTipoLavagem);
            ViewData["Cpf"] = new SelectList(_context.Funcionarios, "Cpf", "Cpf", lavagen.Cpf);
            ViewData["Placa"] = new SelectList(_context.Veiculos, "Placa", "Placa", lavagen.Placa);
            return View(lavagen);
        }

        // POST: Lavagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cpf,Placa,DataLavagem,CodTipoLavagem,Valor")] Lavagen lavagen)
        {
            if (id != lavagen.Cpf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lavagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LavagenExists(lavagen.Cpf))
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
            ViewData["CodTipoLavagem"] = new SelectList(_context.TipoLavagems, "Codigo", "NomeLavagem", lavagen.CodTipoLavagem);
            ViewData["Cpf"] = new SelectList(_context.Funcionarios, "Cpf", "Cpf", lavagen.Cpf);
            ViewData["Placa"] = new SelectList(_context.Veiculos, "Placa", "Placa", lavagen.Placa);
            return View(lavagen);
        }

        // GET: Lavagens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavagen = await _context.Lavagens
                .Include(l => l.CodTipoLavagemNavigation)
                .Include(l => l.CpfNavigation)
                .Include(l => l.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.Cpf == id);
            if (lavagen == null)
            {
                return NotFound();
            }

            return View(lavagen);
        }

        // POST: Lavagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lavagen = await _context.Lavagens.FindAsync(id);
            _context.Lavagens.Remove(lavagen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LavagenExists(string id)
        {
            return _context.Lavagens.Any(e => e.Cpf == id);
        }
    }
}
