using ACME.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ACME;
using Services.ACME;

namespace ACME_ACME.Controllers
{
    public class TipoEmpresaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            TipoEmpresaService tipoEmpresaService = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad;

            listaTipoEmpresaEntidad = tipoEmpresaService.Listar();

            return View(listaTipoEmpresaEntidad);
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

            TipoEmpresaService tipoEmpresaService = new();

            ViewBag.nroPagina = nroPagina;
            ViewBag.totalPaginas = parametro.CalcularPaginas(tipoEmpresaService.Contar(), tamPagina);
            ViewBag.maxPaginasVisibles = maxPaginasVisibles;
    
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad;

            listaTipoEmpresaEntidad = tipoEmpresaService.ObtenerPagina(nroPagina, tamPagina);

            return View(listaTipoEmpresaEntidad);
        }

        [HttpGet]
        public IActionResult BuscarxID(int IDTipoEmpresa)
        {
            TipoEmpresaEntidad? tipoEmpresaEntidad;
            TipoEmpresaService? tipoEmpresaService = new();

            // Obtener lista de tipos de entidad para la vista (Esto aplica para los FK)
            

            tipoEmpresaEntidad = tipoEmpresaService.BuscarxID(IDTipoEmpresa);

            // PKs para navegar
            ViewBag.Inicio = tipoEmpresaService.IDNavegacion(TipoNavegacion.Inicio, 0);
            ViewBag.Fin = tipoEmpresaService.IDNavegacion(TipoNavegacion.Fin, 0);
            ViewBag.Anterior = tipoEmpresaService.IDNavegacion(TipoNavegacion.Anterior, IDTipoEmpresa);
            ViewBag.Siguiente = tipoEmpresaService.IDNavegacion(TipoNavegacion.Siguiente, IDTipoEmpresa);

            return View(tipoEmpresaEntidad);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            TipoEmpresaEntidad tipoEmpresaEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            
            
            return View(tipoEmpresaEntidad);
        }

        [HttpPost]
        public IActionResult Crear(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            if (ModelState.IsValid)
            {
                TipoEmpresaService tipoEmpresaService = new();

                tipoEmpresaService.Crear(tipoEmpresaEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            

            return View(tipoEmpresaEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int IDTipoEmpresa)
        {
            TipoEmpresaEntidad? tipoEmpresaEntidad = new();
            TipoEmpresaService tipoEmpresaService = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            

            tipoEmpresaEntidad = tipoEmpresaService.BuscarxID(IDTipoEmpresa);

            return View(tipoEmpresaEntidad);
        }

        [HttpPost]
        public IActionResult Modificar(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            if (ModelState.IsValid)
            {
                TipoEmpresaService tipoEmpresaService = new();

                tipoEmpresaService.Actualizar(tipoEmpresaEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            

            return View(tipoEmpresaEntidad);
        }

        [HttpGet]
        public IActionResult Anular(int IDTipoEmpresa)
        {
            TipoEmpresaEntidad? tipoEmpresaEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            

            TipoEmpresaService tipoEmpresaService = new();

            tipoEmpresaEntidad = tipoEmpresaService.BuscarxID(IDTipoEmpresa);

            return View(tipoEmpresaEntidad);
        }

        [HttpPost]
        public IActionResult Anular(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            TipoEmpresaService tipoEmpresaService = new();

            tipoEmpresaService.Eliminar(tipoEmpresaEntidad);

            /*
            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            
            */

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FiltrarTipoEmpresa(string filtro)
        {
            TipoEmpresaService tipoEmpresaService = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad;

            listaTipoEmpresaEntidad = tipoEmpresaService.Filtrar(filtro);

            return Json(listaTipoEmpresaEntidad.Select(a => new
            {
                _IDTipoEmpresa = a.IDTipoEmpresa,
                _TipoEmpresa = a.TipoEmpresa
            }));
        }
    }
}
