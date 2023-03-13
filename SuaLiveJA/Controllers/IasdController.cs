using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuaLiveJA.Data;
using SuaLiveJA.Models;
using SuaLiveJA.Models.ViewModels;

namespace SuaLiveJA.Controllers
{
    public class IasdController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IasdController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Iasd
        public async Task<IActionResult> Index()
        {
              return View(await _context.Iasd.ToListAsync());
        }

        // GET: Iasd/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Iasd == null)
            {
                return NotFound();
            }

            var iasd = await _context.Iasd.FirstOrDefaultAsync(m => m.Id == id);
            if (iasd == null)
            {
                return NotFound();
            }

            IasdViewModel iasdModel = new IasdViewModel();
            iasdModel.Id = iasd.Id;
            iasdModel.Name = iasd.Name;

            var contatos = _context.Contato.ToList();
            if (iasd.Contato != null)
            {
                iasdModel.Endereco = iasd.Contato.Endereco;
                iasdModel.Email = iasd.Contato.Email;
                iasdModel.Telefone = iasd.Contato.Telefone;
            }

            return View(iasdModel);
        }

        // GET: Iasd/Create
        public IActionResult Create()
        {
            IasdViewModel iasdModel = new IasdViewModel();
            

            return View(iasdModel);
        }

        // POST: Iasd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IasdViewModel iasdModel)
        {
            Contato contato = new Contato(iasdModel.Endereco, iasdModel.Email, iasdModel.Telefone);
            Iasd iasd = new Iasd(iasdModel.Name, contato);

            try
            {
                _context.Add(iasd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }

        // GET: Iasd/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Iasd == null)
            {
                return NotFound();
            }

            var iasd = await _context.Iasd.FindAsync(id);
            if (iasd == null)
            {
                return NotFound();
            }

            IasdViewModel iasdModel = new IasdViewModel();
            iasdModel.Name = iasd.Name;

            var contatos = _context.Contato.ToList();
            if (iasd.Contato != null)
            {
                iasdModel.Endereco = iasd.Contato.Endereco;
                iasdModel.Email = iasd.Contato.Email;
                iasdModel.Telefone = iasd.Contato.Telefone;
            }

            return View(iasdModel);
        }

        // POST: Iasd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IasdViewModel iasdModel)
        {
            if (id != iasdModel.Id)
            {
                return NotFound();
            }

            Iasd iasd = await _context.Iasd.FindAsync(id);
            if (iasd == null)
            {
                return NotFound();
            }

            var contatos = _context.Contato.ToList();

            iasd.Name = iasdModel.Name;
            iasd.Contato.Endereco = iasdModel.Endereco;
            iasd.Contato.Email = iasdModel.Email;
            iasd.Contato.Telefone = iasdModel.Telefone;
    
            try
            {
                _context.Update(iasd);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IasdExists(iasd.Id))
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

        // GET: Iasd/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Iasd == null)
            {
                return NotFound();
            }

            var iasd = await _context.Iasd
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iasd == null)
            {
                return NotFound();
            }

            return View(iasd);
        }

        // POST: Iasd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Iasd == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Iasd'  is null.");
            }
            var iasd = await _context.Iasd.FindAsync(id);
            if (iasd != null)
            {
                _context.Iasd.Remove(iasd);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IasdExists(int id)
        {
          return _context.Iasd.Any(e => e.Id == id);
        }
    }
}
