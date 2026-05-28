//using AppWebACME.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using Models.ACME;

//namespace AppWebACME.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        //public IActionResult Index()
//        //{
//        //    return View();
//        //}

//        public IActionResult Index()
//        {
//            // Inicializamos el ViewModel para que no sea null
//            var viewModel = new DashboardViewModel();

//            // Llenamos datos de prueba temporales
//            viewModel.TotalRequisiciones = 48;
//            viewModel.TotalAprobadas = 31;
//            viewModel.TotalEmpresasActivas = 6;
//            viewModel.TotalArticulos = 8;

//            // Inicializamos las listas vacías para que los bucles no fallen
//            viewModel.RequisicionesRecientes = new List<Models.ACME.RequisicionEntidad>();
//            viewModel.EmpresasPorTipo = new Dictionary<string, int>
//{
//    { "Cooperativa", 2 },
//    { "S.A. Cerrada", 2 },
//    { "S.R.L.", 1 },
//    { "Sucursal", 1 }
//};

//            // CRUCIAL: Pasamos el objeto a la vista
//            return View(viewModel);
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    //}
//}

using AppWebACME.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Models.ACME;
using Services.ACME;

namespace AppWebACME.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RequisicionService _requisicionService;
        private readonly EmpresaService _empresaService;
        private readonly ArticuloService _articuloService;

        public HomeController(
            ILogger<HomeController> logger,
            RequisicionService requisicionService,
            EmpresaService empresaService,
            ArticuloService articuloService)
        {
            _logger = logger;
            _requisicionService = requisicionService;
            _empresaService = empresaService;
            _articuloService = articuloService;
        }

        // ✅ Ya no es async — los servicios son síncronos
        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel();

            try
            {
                // ✅ Nombre correcto: Listar() — sin await, son síncronos
                List<RequisicionEntidad>? listaRequisiciones = _requisicionService.Listar();
                List<EmpresaEntidad>? listaEmpresas = _empresaService.Listar();
                List<ArticuloEntidad>? listaArticulos = _articuloService.Listar();

                // ── KPIs ──────────────────────────────────────────────
                viewModel.TotalRequisiciones = listaRequisiciones?.Count ?? 0;
                viewModel.TotalArticulos = listaArticulos?.Count ?? 0;
                viewModel.TotalAprobadas = listaRequisiciones?
                                                    .Count(r => r.Aprobada) ?? 0;
                viewModel.TotalEmpresasActivas = listaEmpresas?
                                                    .Count(e => e.Activo) ?? 0;

                // ── Tabla: 5 requisiciones más recientes ──────────────
                viewModel.RequisicionesRecientes = listaRequisiciones?
                    .OrderByDescending(r => r.FechaEmision)
                    .Take(5)
                    .ToList()
                    ?? new List<RequisicionEntidad>();

                // ── Gráfico: empresas agrupadas por tipo ──────────────
                viewModel.EmpresasPorTipo = listaEmpresas?
                    .Where(e => !string.IsNullOrEmpty(e.Empresa))
                    .GroupBy(e => e.Empresa)
                    .ToDictionary(g => g.Key, g => g.Count())
                    ?? new Dictionary<string, int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar datos del Dashboard.");
                viewModel.RequisicionesRecientes = new List<RequisicionEntidad>();
                viewModel.EmpresasPorTipo = new Dictionary<string, int>();
            }

            return View(viewModel);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}