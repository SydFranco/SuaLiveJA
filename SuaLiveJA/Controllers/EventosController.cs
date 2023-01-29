using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuaLiveJA.Data;
using SuaLiveJA.Models;
using SuaLiveJA.Models.ViewModel;

namespace SuaLiveJA.Controllers
{
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var evento = _context.Evento.ToList();
            var secoes = _context.Secao.ToList();
              return View(evento);
        }

        // GET: Eventos/Solicitar Publicação/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            EventosViewModel eventosModel = new EventosViewModel();
            eventosModel.SecoesSelect = new List<SelectListItem> { new SelectListItem { Text = "Selecione uma Seção.", Value = "" } };

            var Secoes = _context.Secao.ToList();
            foreach (Secao secao in Secoes)
            {
                eventosModel.SecoesSelect.Add(new SelectListItem { Text = secao.Descricao, Value = secao.Id.ToString() });
            }
            return View(eventosModel);
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            Secao secao = _context.Secao.Where(x => x.Id == evento.Secao.Id).FirstOrDefault();
            Evento evento1 = new Evento(evento.Descricao, evento.Data_Hora, evento.Link_URL, evento.Post, secao);
            try
            {
                _context.Add(evento1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ex);

            }
            
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }
            
            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            EventosViewModel eventosModel = new EventosViewModel();
            eventosModel.SecoesSelect = new List<SelectListItem> { new SelectListItem { Text = "Selecione uma Seção.", Value = "" } };
            var secoes = _context.Secao.ToList();
            foreach (Secao secao in secoes)
            {
                eventosModel.SecoesSelect.Add(new SelectListItem { Text = secao.Descricao, Value = secao.Id.ToString() });
            }

            eventosModel.Descricao = evento.Descricao;
            eventosModel.Data_Hora = evento.Data_Hora;
            eventosModel.Link_URL = evento.Link_URL;
            eventosModel.Post = evento.Post;
            eventosModel.Secao = evento.Secao;

            return View(eventosModel);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventosViewModel eventosModel)

        {
            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            Secao secao = _context.Secao.Where(x => x.Id == eventosModel.Secao.Id).FirstOrDefault();

            evento.Descricao = eventosModel.Descricao;
            evento.Data_Hora = eventosModel.Data_Hora;
            evento.Link_URL = eventosModel.Link_URL;
            evento.Post = eventosModel.Post;
            evento.Secao = secao;

            try
            {
                _context.Update(evento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(evento.Id))
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

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento .FirstOrDefaultAsync(m => m.Id == id);
            var secoes = _context.Secao.ToList();
            EventosViewModel eventosModel = new EventosViewModel();

            eventosModel.Id = evento.Id;
            eventosModel.Descricao = evento.Descricao;
            eventosModel.Data_Hora = evento.Data_Hora;
            eventosModel.Link_URL = evento.Link_URL;
            eventosModel.Post = evento.Post;
            eventosModel.Secao = evento.Secao;

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Evento'  is null.");
            }
            var evento = await _context.Evento.FindAsync(id);
            if (evento != null)
            {
                _context.Evento.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return _context.Evento.Any(e => e.Id == id);
        }
    }
}
