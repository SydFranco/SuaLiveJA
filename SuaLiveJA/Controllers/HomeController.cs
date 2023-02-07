using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using SuaLiveJA.Data;
using SuaLiveJA.Models;
using System.Diagnostics;
using System.Web;
using System.Net.NetworkInformation;
using System.Web.Helpers;


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
        public async Task<IActionResult> Index(string BuscaEvento, DateTime datax, string currentFilter, int? page, string sortOrder)
        {
            if (_context.Evento == null)
            {
                return Problem("Nulo");
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var eventos = from e in _context.Evento
                          select e;

            // Ordenação Ok
            switch(sortOrder)
            {
                case "name_desc":
                    eventos = eventos.OrderByDescending(s => s.Descricao);
                    break;
                case "Date":
                    eventos = eventos.OrderBy(s => s.Data_Hora);
                    break;
                case "date_desc":
                    eventos = eventos.OrderByDescending(s => s.Data_Hora);
                    break;
                default:
                    eventos = eventos.OrderBy(s => s.Descricao);
                    break;
            }

            // Paginação
            if (BuscaEvento != null)
            {
                page = 1;
            }
            else
            {
                BuscaEvento = currentFilter;
            }

            
            if (datax !=null )
            {
               
                eventos = eventos.Where(s => s.Data_Hora >= datax );

            }
            if (!string.IsNullOrEmpty(BuscaEvento))
            {
                eventos = eventos.Where(s => s.Descricao!.Contains(BuscaEvento));
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(eventos.ToPagedList(pageNumber, pageSize));
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