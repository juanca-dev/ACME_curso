using ACME.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ACME;
using Services.ACME;

namespace ACME_ACME.Controllers
{
    public class UnidadMedidaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            UnidadMedidaService unidadMedidaService = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad;

            listaUnidadMedidaEntidad = unidadMedidaService.Listar();

            return View(listaUnidadMedidaEntidad);
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

            UnidadMedidaService unidadMedidaService = new();

            ViewBag.nroPagina = nroPagina;
            ViewBag.totalPaginas = parametro.CalcularPaginas(unidadMedidaService.Contar(), tamPagina);
            ViewBag.maxPaginasVisibles = maxPaginasVisibles;
    
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad;

            listaUnidadMedidaEntidad = unidadMedidaService.ObtenerPagina(nroPagina, tamPagina);

            return View(listaUnidadMedidaEntidad);
        }

        [HttpGet]
        public IActionResult BuscarxID(int IDUnidadMedida)
        {
            UnidadMedidaEntidad? unidadMedidaEntidad;
            UnidadMedidaService? unidadMedidaService = new();

            // Obtener lista de tipos de entidad para la vista (Esto aplica para los FK)
            

            unidadMedidaEntidad = unidadMedidaService.BuscarxID(IDUnidadMedida);

            // PKs para navegar
            ViewBag.Inicio = unidadMedidaService.IDNavegacion(TipoNavegacion.Inicio, 0);
            ViewBag.Fin = unidadMedidaService.IDNavegacion(TipoNavegacion.Fin, 0);
            ViewBag.Anterior = unidadMedidaService.IDNavegacion(TipoNavegacion.Anterior, IDUnidadMedida);
            ViewBag.Siguiente = unidadMedidaService.IDNavegacion(TipoNavegacion.Siguiente, IDUnidadMedida);

            return View(unidadMedidaEntidad);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            UnidadMedidaEntidad unidadMedidaEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            
            
            return View(unidadMedidaEntidad);
        }

        [HttpPost]
        public IActionResult Crear(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            if (ModelState.IsValid)
            {
                UnidadMedidaService unidadMedidaService = new();

                unidadMedidaService.Crear(unidadMedidaEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            

            return View(unidadMedidaEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int IDUnidadMedida)
        {
            UnidadMedidaEntidad? unidadMedidaEntidad = new();
            UnidadMedidaService unidadMedidaService = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            

            unidadMedidaEntidad = unidadMedidaService.BuscarxID(IDUnidadMedida);

            return View(unidadMedidaEntidad);
        }

        [HttpPost]
        public IActionResult Modificar(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            if (ModelState.IsValid)
            {
                UnidadMedidaService unidadMedidaService = new();

                unidadMedidaService.Actualizar(unidadMedidaEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            

            return View(unidadMedidaEntidad);
        }

        [HttpGet]
        public IActionResult Anular(int IDUnidadMedida)
        {
            UnidadMedidaEntidad? unidadMedidaEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            

            UnidadMedidaService unidadMedidaService = new();

            unidadMedidaEntidad = unidadMedidaService.BuscarxID(IDUnidadMedida);

            return View(unidadMedidaEntidad);
        }

        [HttpPost]
        public IActionResult Anular(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            UnidadMedidaService unidadMedidaService = new();

            unidadMedidaService.Eliminar(unidadMedidaEntidad);

            /*
            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            
            */

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FiltrarUnidadMedida(string filtro)
        {
            UnidadMedidaService unidadMedidaService = new();
            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad;

            listaUnidadMedidaEntidad = unidadMedidaService.Filtrar(filtro);

            return Json(listaUnidadMedidaEntidad.Select(a => new
            {
                _IDUnidadMedida = a.IDUnidadMedida,
                _UnidadMedida = a.UnidadMedida
            }));
        }
    }
}
