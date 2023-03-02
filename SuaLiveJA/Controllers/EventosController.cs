using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
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
        public async Task<IActionResult> Index(string EventoBusca,DateTime date, string status )
        {
            if (_context.Evento == null)
            {
                return Problem("Não Existe");
            }
            var eventos = _context.Evento.ToList();
            if (date != null)
            {

                eventos = eventos.Where(s => s.Data_Hora >= date).ToList();

            }
            if (!string.IsNullOrEmpty(EventoBusca))
            {
                eventos = eventos.Where(s => s.Descricao!.Contains(EventoBusca)).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                EStatus myEnum = (EStatus)Enum.Parse(typeof(EStatus), status);
                eventos = eventos.Where(s => s.Status == myEnum).ToList();
            }
            var secoes = _context.Secao.ToList();

            List<EventosViewModel> listaEventosView = new List<EventosViewModel>();

            foreach (var evento in eventos)
            {
                EventosViewModel eventosView = new EventosViewModel();
                eventosView.Id = evento.Id;
                eventosView.Descricao = evento.Descricao;
                eventosView.Data_Hora = evento.Data_Hora;
                eventosView.Link_URL = evento.Link_URL;
                eventosView.Post = evento.Post;
                eventosView.Secao = evento.Secao;
                eventosView.Status = evento.Status;
                listaEventosView.Add(eventosView);
            }


            var enumData = from EStatus e in Enum.GetValues(typeof(EStatus))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            ViewBag.EnumList = new SelectList(enumData, "ID", "Name");

            return View(listaEventosView);
        }

        // GET: Eventos/Solicitar Publicação/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FirstOrDefaultAsync(m => m.Id == id);
            var secoes = _context.Secao.ToList();
            EventosViewModel eventosView = new EventosViewModel();

            eventosView.Id = evento.Id;
            eventosView.Descricao = evento.Descricao;
            eventosView.Data_Hora = evento.Data_Hora;
            eventosView.Link_URL = evento.Link_URL;
            eventosView.Post = evento.Post;
            eventosView.Secao = evento.Secao;
            eventosView.Status = evento.Status;

            if (evento == null)
            {
                return NotFound();
            }

            return View(eventosView);
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
        public async Task<IActionResult> Create(EventosViewModel viewModel)
        {
            Secao secao = _context.Secao.Where(x => x.Id == viewModel.Secao.Id).FirstOrDefault();
            Evento evento = new Evento(viewModel.Descricao, viewModel.Data_Hora, viewModel.Link_URL, viewModel.Post, secao);
            try
            {
                _context.Add(evento);
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
            eventosModel.Status = evento.Status;

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
            evento.Status = eventosModel.Status;

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
            eventosModel.Status = evento.Status;

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

        // GET: Eventos/SolicitarAprovacao/6
        public async Task<IActionResult> SolicitarAprovacao(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }
            var secaos = _context.Secao.ToList();

            return View(evento);
        }

        // POST: Eventos/SolicitarConfirmacao/6
        public async Task<IActionResult> SolicitarConfirmacao(int id)
        {
            // verificar se  evento no contexto é null
            if (_context.Evento == null)
            {
                return Problem("Entity set ' ApplicationDbContext.Evento'  is null.");
            }
            // pegar evento pelo Id
            var evento = await _context.Evento.FindAsync(id);
            // verificar se é null
            if (evento == null)
            {
                return Problem("Evento não encontrado.");
            }
            else
            {
                // colocar status em evento
                evento.Status = EStatus.EmAprovacao;

                // atualizar o contexto
                _context.Update(evento);
                //salvar no banco
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Eventos/Publicar/7
        public async Task<IActionResult> Publicar(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }
            var secaos = _context.Secao.ToList();


            return View(evento);
        }

        // POST: Eventos/Publicar/7
        public async Task<IActionResult> PublicarConfirmacao(int id)
        {
            // verificar se  evento no contexto é null
            if (_context.Evento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Evento'  is null.");
            }
            // pegar evento pelo Id
            var evento = await _context.Evento.FindAsync(id);
            // verificar se é null
            if (evento == null)
            {
                return Problem("evento não publicado.");
            }
            else
            {
                // colocar status de Publicado no evento
                evento.Status = EStatus.Publicado;

                // atualizar o contexto
                _context.Update(evento);
                //salvar no banco
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public bool EventoExistss(int id)
        {
            return _context.Evento.Any(e => e.Id == id);
        }
    }
}
