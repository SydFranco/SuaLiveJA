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

            var iasd = await _context.Iasd
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iasd == null)
            {
                return NotFound();
            }

            return View(iasd);
        }

        // GET: Iasd/Create
        public IActionResult Create()
        {
            IasdViewModel iasdModel = new IasdViewModel();
            iasdModel.ContatosSelect = new List<SelectListItem> { new SelectListItem { Text = "Selecione uma Seção", Value = "" } };

            var contacts = _context.Contato.ToList();
            foreach (Contato contato in contacts)
            {
                iasdModel.ContatosSelect.Add(new SelectListItem { Text = contato.Endereco , Value = contato.Id.ToString() });
            }

            return View(iasdModel);
        }

        // POST: Iasd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IasdViewModel iasdModel)
        {
            Contato contatos = _context.Contato.Where(x => x.Id == iasdModel.Contato.Id).FirstOrDefault();

            Iasd iasd = new Iasd(iasdModel.Name, contatos);

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
            return View(iasd);
        }

        // POST: Iasd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Iasd iasd)
        {
            if (id != iasd.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(iasd);
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
