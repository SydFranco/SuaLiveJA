using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuaLiveJA.Data;
using SuaLiveJA.Models;
using SuaLiveJA.Models.ViewModel;
using System.Diagnostics;
using PagedList.Mvc;

namespace SuaLiveJA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
       

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index(string BuscaEvento, DateTime datax)
        {
            if (_context.Evento == null)
            {
                return Problem("Não há eventos disponíveis.");
            }
            var eventos = from e in _context.Evento                  
                          select e;

            var eventosPassados = from e in _context.Evento
                                  where e.Data_Hora < DateTime.Now
                                  orderby e.Data_Hora descending
                                  select e;

            var eventosPassados2 = eventosPassados.ToList();

            ViewBag.ListEventosPassados = eventosPassados.ToList().Take(6);

            if (datax != DateTime.MinValue)
            {
               eventos = eventos.Where(s => s.Data_Hora >= datax );
            }

            if (!string.IsNullOrEmpty(BuscaEvento))
            {
                eventos = eventos.Where(s => s.Descricao!.Contains(BuscaEvento));
            }

            if ( eventos != null)
            {
                eventos = eventos.Where(s => s.Status == EStatus.Publicado);
            }

            return View(await eventos.ToListAsync());
        }

        // public IActionResult Index(int page = 1)
        // {
        // var dataProducts = _context.Evento.GetPaged(page, 10);
        //     return View(dataProducts);
        // }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}