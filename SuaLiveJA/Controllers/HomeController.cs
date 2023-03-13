using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuaLiveJA.Data;
using SuaLiveJA.Models;
using System.Diagnostics;

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
                return Problem("Nulo");
            }
            var eventos = from e in _context.Evento
                          select e;

            if (datax !=null )
            {
               
                eventos = eventos.Where(s => s.Data_Hora >= datax );

            }
            if (!string.IsNullOrEmpty(BuscaEvento))
            {
                eventos = eventos.Where(s => s.Descricao!.Contains(BuscaEvento));
            }

            if (string.IsNullOrEmpty(""))
                    {
                        eventos = eventos.Where(e => e.Data_Hora <= DateTime.Now);
                    }    
                return View(await eventos.ToListAsync());
        }

        public IActionResult Index2()
        {
            if (_context.Evento == null)
            {
                return Problem("Nulo");
            }
            var eventos = from e in _context.Evento
                           select e;

               if (string.IsNullOrEmpty("") )
                {
                    eventos = eventos.Where(e => e.Data_Hora <= DateTime.Now);
                }
                
            return View(eventos.ToList());
        }

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