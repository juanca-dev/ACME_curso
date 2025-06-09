using ACME.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ACME;
using Services.ACME;

namespace ACME_ACME.Controllers
{
    public class RequisicionDetalleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            RequisicionDetalleService requisicionDetalleService = new();
            List<RequisicionDetalleEntidad>? listaRequisicionDetalleEntidad;

            listaRequisicionDetalleEntidad = requisicionDetalleService.Listar();

            return View(listaRequisicionDetalleEntidad);
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

            RequisicionDetalleService requisicionDetalleService = new();

            ViewBag.nroPagina = nroPagina;
            ViewBag.totalPaginas = parametro.CalcularPaginas(requisicionDetalleService.Contar(), tamPagina);
            ViewBag.maxPaginasVisibles = maxPaginasVisibles;
    
            List<RequisicionDetalleEntidad>? listaRequisicionDetalleEntidad;

            listaRequisicionDetalleEntidad = requisicionDetalleService.ObtenerPagina(nroPagina, tamPagina);

            return View(listaRequisicionDetalleEntidad);
        }

        [HttpGet]
        public IActionResult BuscarxID(int IDRequisicionDetalle)
        {
            RequisicionDetalleEntidad? requisicionDetalleEntidad;
            RequisicionDetalleService? requisicionDetalleService = new();

            // Obtener lista de tipos de entidad para la vista (Esto aplica para los FK)
            ArticuloService articuloService0FK = new();
            List<ArticuloEntidad>? listaArticuloEntidad0 = articuloService0FK.Listar();
            ViewBag.ListaArticuloEntidad0 = new SelectList(listaArticuloEntidad0, "IDArticulo", "Articulo");
		RequisicionService requisicionService1FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad1 = requisicionService1FK.Listar();
            ViewBag.ListaRequisicionEntidad1 = new SelectList(listaRequisicionEntidad1, "IDRequisicion", "NroRequisicion");


            requisicionDetalleEntidad = requisicionDetalleService.BuscarxID(IDRequisicionDetalle);

            // PKs para navegar
            ViewBag.Inicio = requisicionDetalleService.IDNavegacion(TipoNavegacion.Inicio, 0);
            ViewBag.Fin = requisicionDetalleService.IDNavegacion(TipoNavegacion.Fin, 0);
            ViewBag.Anterior = requisicionDetalleService.IDNavegacion(TipoNavegacion.Anterior, IDRequisicionDetalle);
            ViewBag.Siguiente = requisicionDetalleService.IDNavegacion(TipoNavegacion.Siguiente, IDRequisicionDetalle);

            return View(requisicionDetalleEntidad);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            RequisicionDetalleEntidad requisicionDetalleEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            ArticuloService articuloService0FK = new();
            List<ArticuloEntidad>? listaArticuloEntidad0 = articuloService0FK.Listar();
            ViewBag.ListaArticuloEntidad0 = new SelectList(listaArticuloEntidad0, "IDArticulo", "Articulo");
		RequisicionService requisicionService1FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad1 = requisicionService1FK.Listar();
            ViewBag.ListaRequisicionEntidad1 = new SelectList(listaRequisicionEntidad1, "IDRequisicion", "NroRequisicion");

            
            return View(requisicionDetalleEntidad);
        }

        [HttpPost]
        public IActionResult Crear(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            if (ModelState.IsValid)
            {
                RequisicionDetalleService requisicionDetalleService = new();

                requisicionDetalleService.Crear(requisicionDetalleEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            ArticuloService articuloService0FK = new();
            List<ArticuloEntidad>? listaArticuloEntidad0 = articuloService0FK.Listar();
            ViewBag.ListaArticuloEntidad0 = new SelectList(listaArticuloEntidad0, "IDArticulo", "Articulo");
		RequisicionService requisicionService1FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad1 = requisicionService1FK.Listar();
            ViewBag.ListaRequisicionEntidad1 = new SelectList(listaRequisicionEntidad1, "IDRequisicion", "NroRequisicion");


            return View(requisicionDetalleEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int IDRequisicionDetalle)
        {
            RequisicionDetalleEntidad? requisicionDetalleEntidad = new();
            RequisicionDetalleService requisicionDetalleService = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            ArticuloService articuloService0FK = new();
            List<ArticuloEntidad>? listaArticuloEntidad0 = articuloService0FK.Listar();
            ViewBag.ListaArticuloEntidad0 = new SelectList(listaArticuloEntidad0, "IDArticulo", "Articulo");
		RequisicionService requisicionService1FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad1 = requisicionService1FK.Listar();
            ViewBag.ListaRequisicionEntidad1 = new SelectList(listaRequisicionEntidad1, "IDRequisicion", "NroRequisicion");


            requisicionDetalleEntidad = requisicionDetalleService.BuscarxID(IDRequisicionDetalle);

            return View(requisicionDetalleEntidad);
        }

        [HttpPost]
        public IActionResult Modificar(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            if (ModelState.IsValid)
            {
                RequisicionDetalleService requisicionDetalleService = new();

                requisicionDetalleService.Actualizar(requisicionDetalleEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            ArticuloService articuloService0FK = new();
            List<ArticuloEntidad>? listaArticuloEntidad0 = articuloService0FK.Listar();
            ViewBag.ListaArticuloEntidad0 = new SelectList(listaArticuloEntidad0, "IDArticulo", "Articulo");
		RequisicionService requisicionService1FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad1 = requisicionService1FK.Listar();
            ViewBag.ListaRequisicionEntidad1 = new SelectList(listaRequisicionEntidad1, "IDRequisicion", "NroRequisicion");


            return View(requisicionDetalleEntidad);
        }

        [HttpGet]
        public IActionResult Anular(int IDRequisicionDetalle)
        {
            RequisicionDetalleEntidad? requisicionDetalleEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            ArticuloService articuloService0FK = new();
            List<ArticuloEntidad>? listaArticuloEntidad0 = articuloService0FK.Listar();
            ViewBag.ListaArticuloEntidad0 = new SelectList(listaArticuloEntidad0, "IDArticulo", "Articulo");
		RequisicionService requisicionService1FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad1 = requisicionService1FK.Listar();
            ViewBag.ListaRequisicionEntidad1 = new SelectList(listaRequisicionEntidad1, "IDRequisicion", "NroRequisicion");


            RequisicionDetalleService requisicionDetalleService = new();

            requisicionDetalleEntidad = requisicionDetalleService.BuscarxID(IDRequisicionDetalle);

            return View(requisicionDetalleEntidad);
        }

        [HttpPost]
        public IActionResult Anular(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            RequisicionDetalleService requisicionDetalleService = new();

            requisicionDetalleService.Eliminar(requisicionDetalleEntidad);

            /*
            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            ArticuloService articuloService0FK = new();
            List<ArticuloEntidad>? listaArticuloEntidad0 = articuloService0FK.Listar();
            ViewBag.ListaArticuloEntidad0 = new SelectList(listaArticuloEntidad0, "IDArticulo", "Articulo");
		RequisicionService requisicionService1FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad1 = requisicionService1FK.Listar();
            ViewBag.ListaRequisicionEntidad1 = new SelectList(listaRequisicionEntidad1, "IDRequisicion", "NroRequisicion");

            */

            return RedirectToAction("Index");
        }

        
    }
}
