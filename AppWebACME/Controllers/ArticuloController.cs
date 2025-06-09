using ACME.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ACME;
using Services.ACME;

namespace ACME.Controllers
{
    public class ArticuloController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ArticuloService articuloService = new();
            List<ArticuloEntidad>? listaArticuloEntidad;

            listaArticuloEntidad = articuloService.Listar();

            return View(listaArticuloEntidad);
        }

        [HttpGet]
        public IActionResult IndexPaginado(int? pagina)
        {
            // Obtener el tamaño de página desde el archivo de configuración
            IConfigurationRoot configuracion = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    
            Parametro parametro = new Parametro(configuracion);
            int tamPagina = parametro.ObtenerFilasPorPagina();
            int maxPaginasVisibles = parametro.ObtenerMaxPaginasVisibles();

            int nroPagina = pagina ?? 1;

            ArticuloService articuloService = new();

            ViewBag.nroPagina = nroPagina;
            ViewBag.totalPaginas = parametro.CalcularPaginas(articuloService.Contar(), tamPagina);
            ViewBag.maxPaginasVisibles = maxPaginasVisibles;
    
            List<ArticuloEntidad> listaArticuloEntidad;

            listaArticuloEntidad = articuloService.ObtenerPagina(nroPagina, tamPagina) ?? new List<ArticuloEntidad>();

            return View(listaArticuloEntidad);
        }

        [HttpGet]
        public IActionResult BuscarxID(int IDArticulo)
        {
            ArticuloEntidad? articuloEntidad;
            ArticuloService? articuloService = new();

            // Obtener lista de tipos de entidad para la vista (Esto aplica para los FK)
            UnidadMedidaService unidadMedidaService0FK = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad0 = unidadMedidaService0FK.Listar();
            ViewBag.ListaUnidadMedidaEntidad0 = new SelectList(listaUnidadMedidaEntidad0, "IDUnidadMedida", "UnidadMedida");


            articuloEntidad = articuloService.BuscarxID(IDArticulo);

            // PKs para navegar
            ViewBag.Inicio = articuloService.IDNavegacion(TipoNavegacion.Inicio, 0);
            ViewBag.Fin = articuloService.IDNavegacion(TipoNavegacion.Fin, 0);
            ViewBag.Anterior = articuloService.IDNavegacion(TipoNavegacion.Anterior, IDArticulo);
            ViewBag.Siguiente = articuloService.IDNavegacion(TipoNavegacion.Siguiente, IDArticulo);

            return View(articuloEntidad);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ArticuloEntidad articuloEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            UnidadMedidaService unidadMedidaService0FK = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad0 = unidadMedidaService0FK.Listar();
            ViewBag.ListaUnidadMedidaEntidad0 = new SelectList(listaUnidadMedidaEntidad0, "IDUnidadMedida", "UnidadMedida");

            
            return View(articuloEntidad);
        }

        [HttpPost]
        public IActionResult Crear(ArticuloEntidad articuloEntidad)
        {
            if (ModelState.IsValid)
            {
                ArticuloService articuloService = new();

                articuloService.Crear(articuloEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            UnidadMedidaService unidadMedidaService0FK = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad0 = unidadMedidaService0FK.Listar();
            ViewBag.ListaUnidadMedidaEntidad0 = new SelectList(listaUnidadMedidaEntidad0, "IDUnidadMedida", "UnidadMedida");


            return View(articuloEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int IDArticulo)
        {
            ArticuloEntidad? articuloEntidad = new();
            ArticuloService articuloService = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            UnidadMedidaService unidadMedidaService0FK = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad0 = unidadMedidaService0FK.Listar();
            ViewBag.ListaUnidadMedidaEntidad0 = new SelectList(listaUnidadMedidaEntidad0, "IDUnidadMedida", "UnidadMedida");


            articuloEntidad = articuloService.BuscarxID(IDArticulo);

            return View(articuloEntidad);
        }

        [HttpPost]
        public IActionResult Modificar(ArticuloEntidad articuloEntidad)
        {
            if (ModelState.IsValid)
            {
                ArticuloService articuloService = new();

                articuloService.Actualizar(articuloEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            UnidadMedidaService unidadMedidaService0FK = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad0 = unidadMedidaService0FK.Listar();
            ViewBag.ListaUnidadMedidaEntidad0 = new SelectList(listaUnidadMedidaEntidad0, "IDUnidadMedida", "UnidadMedida");


            return View(articuloEntidad);
        }

        [HttpGet]
        public IActionResult Anular(int IDArticulo)
        {
            ArticuloEntidad? articuloEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            UnidadMedidaService unidadMedidaService0FK = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad0 = unidadMedidaService0FK.Listar();
            ViewBag.ListaUnidadMedidaEntidad0 = new SelectList(listaUnidadMedidaEntidad0, "IDUnidadMedida", "UnidadMedida");


            ArticuloService articuloService = new();

            articuloEntidad = articuloService.BuscarxID(IDArticulo);

            return View(articuloEntidad);
        }

        [HttpPost]
        public IActionResult Anular(ArticuloEntidad articuloEntidad)
        {
            ArticuloService articuloService = new();

            articuloService.Eliminar(articuloEntidad);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FiltrarArticulo(string filtro)
        {
            ArticuloService articuloService = new();
            List<ArticuloEntidad>? listaArticuloEntidad;

           listaArticuloEntidad = articuloService.Filtrar(filtro);

            return Json(listaArticuloEntidad.Select(a => new
            {
                _IDArticulo = a.IDArticulo,
                _Articulo = a.Articulo
            }));
        }
    }
}
