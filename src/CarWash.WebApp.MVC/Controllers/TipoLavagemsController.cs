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
    public class TipoLavagemsController : Controller
    {
        private readonly carwashContext _context;

        public TipoLavagemsController(carwashContext context)
        {
            _context = context;
        }

        // GET: TipoLavagems
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoLavagems.ToListAsync());
        }

        // GET: TipoLavagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoLavagem = await _context.TipoLavagems
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tipoLavagem == null)
            {
                return NotFound();
            }

            return View(tipoLavagem);
        }

        // GET: TipoLavagems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoLavagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,NomeLavagem")] TipoLavagem tipoLavagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoLavagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoLavagem);
        }

        // GET: TipoLavagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoLavagem = await _context.TipoLavagems.FindAsync(id);
            if (tipoLavagem == null)
            {
                return NotFound();
            }
            return View(tipoLavagem);
        }

        // POST: TipoLavagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,NomeLavagem")] TipoLavagem tipoLavagem)
        {
            if (id != tipoLavagem.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoLavagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoLavagemExists(tipoLavagem.Codigo))
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
            return View(tipoLavagem);
        }

        // GET: TipoLavagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoLavagem = await _context.TipoLavagems
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tipoLavagem == null)
            {
                return NotFound();
            }

            return View(tipoLavagem);
        }

        // POST: TipoLavagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoLavagem = await _context.TipoLavagems.FindAsync(id);
            _context.TipoLavagems.Remove(tipoLavagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoLavagemExists(int id)
        {
            return _context.TipoLavagems.Any(e => e.Codigo == id);
        }
    }
}
