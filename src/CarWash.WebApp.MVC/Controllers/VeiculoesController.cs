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
    public class VeiculoesController : Controller
    {
        private readonly carwashContext _context;

        public VeiculoesController(carwashContext context)
        {
            _context = context;
        }

        // GET: Veiculoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Veiculos.ToListAsync());
        }

        // GET: Veiculoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // GET: Veiculoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Marca,Modelo,Ano")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        // GET: Veiculoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Placa,Marca,Modelo,Ano")] Veiculo veiculo)
        {
            if (id != veiculo.Placa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Placa))
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
            return View(veiculo);
        }

        // GET: Veiculoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(string id)
        {
            return _context.Veiculos.Any(e => e.Placa == id);
        }
    }
}
