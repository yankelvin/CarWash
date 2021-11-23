using CarWash.WebApp.MVC.Models;
using CarWash.WebApp.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var dados = await carwashContext.ToListAsync();

            var totalArrecadado = dados.Sum(d => d.Valor);

            ViewData["Cpf"] = new SelectList(_context.Funcionarios, "Cpf", "Cpf");

            return View(new RelatorioViewModel { Lavagens = dados, TotalArrecadado = totalArrecadado });
        }

        [HttpPost]
        public async Task<IActionResult> ObterRelatorio(DateTime dataLavagem, string cpfFuncionario)
        {
            var carwashContext = _context.Lavagens.Include(l => l.CodTipoLavagemNavigation).Include(l => l.CpfNavigation).Include(l => l.PlacaNavigation);

            var dados = await carwashContext.Where(c => (!new DateTime().Equals(dataLavagem) && c.DataLavagem.Equals(dataLavagem))
                                                         || (!string.IsNullOrEmpty(cpfFuncionario) && c.Cpf.Equals(cpfFuncionario))).ToListAsync();

            if (new DateTime().Equals(dataLavagem) && string.IsNullOrEmpty(cpfFuncionario))
                dados = await carwashContext.ToListAsync();

            var totalArrecadado = dados.Sum(d => d.Valor);

            ViewData["Cpf"] = new SelectList(_context.Funcionarios, "Cpf", "Cpf");

            return PartialView("ObterRelatorio", new RelatorioViewModel { Lavagens = dados, TotalArrecadado = totalArrecadado });
        }

        // GET: Lavagens/Details/5
        public async Task<IActionResult> Details(string cpf, string placa, DateTime dataLavagem)
        {

            var lavagen = await _context.Lavagens
                .Include(l => l.CodTipoLavagemNavigation)
                .Include(l => l.CpfNavigation)
                .Include(l => l.PlacaNavigation)
                .FirstOrDefaultAsync(p => p.Cpf.Equals(cpf) && p.Placa.Equals(placa) && p.DataLavagem.Equals(dataLavagem));

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
        public async Task<IActionResult> Edit(string cpf, string placa, DateTime dataLavagem)
        {

            var lavagen = await _context.Lavagens.Where(p => p.Cpf.Equals(cpf) && p.Placa.Equals(placa) && p.DataLavagem.Equals(dataLavagem)).FirstAsync();

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
        public async Task<IActionResult> Delete(string cpf, string placa, DateTime dataLavagem)
        {
            var lavagen = await _context.Lavagens
                .Include(l => l.CodTipoLavagemNavigation)
                .Include(l => l.CpfNavigation)
                .Include(l => l.PlacaNavigation)
                .FirstOrDefaultAsync(p => p.Cpf.Equals(cpf) && p.Placa.Equals(placa) && p.DataLavagem.Equals(dataLavagem));

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
