using ACME.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ACME;
using Services.ACME;

namespace ACME_ACME.Controllers
{
    public class RequisicionAnotacionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            RequisicionAnotacionService requisicionAnotacionService = new();
            List<RequisicionAnotacionEntidad>? listaRequisicionAnotacionEntidad;

            listaRequisicionAnotacionEntidad = requisicionAnotacionService.Listar();

            return View(listaRequisicionAnotacionEntidad);
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

            RequisicionAnotacionService requisicionAnotacionService = new();

            ViewBag.nroPagina = nroPagina;
            ViewBag.totalPaginas = parametro.CalcularPaginas(requisicionAnotacionService.Contar(), tamPagina);
            ViewBag.maxPaginasVisibles = maxPaginasVisibles;
    
            List<RequisicionAnotacionEntidad>? listaRequisicionAnotacionEntidad;

            listaRequisicionAnotacionEntidad = requisicionAnotacionService.ObtenerPagina(nroPagina, tamPagina);

            return View(listaRequisicionAnotacionEntidad);
        }

        [HttpGet]
        public IActionResult BuscarxID(int IDRequisicionAnotacion)
        {
            RequisicionAnotacionEntidad? requisicionAnotacionEntidad;
            RequisicionAnotacionService? requisicionAnotacionService = new();

            // Obtener lista de tipos de entidad para la vista (Esto aplica para los FK)
            RequisicionService requisicionService0FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad0 = requisicionService0FK.Listar();
            ViewBag.ListaRequisicionEntidad0 = new SelectList(listaRequisicionEntidad0, "IDRequisicion", "NroRequisicion");


            requisicionAnotacionEntidad = requisicionAnotacionService.BuscarxID(IDRequisicionAnotacion);

            // PKs para navegar
            ViewBag.Inicio = requisicionAnotacionService.IDNavegacion(TipoNavegacion.Inicio, 0);
            ViewBag.Fin = requisicionAnotacionService.IDNavegacion(TipoNavegacion.Fin, 0);
            ViewBag.Anterior = requisicionAnotacionService.IDNavegacion(TipoNavegacion.Anterior, IDRequisicionAnotacion);
            ViewBag.Siguiente = requisicionAnotacionService.IDNavegacion(TipoNavegacion.Siguiente, IDRequisicionAnotacion);

            return View(requisicionAnotacionEntidad);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            RequisicionAnotacionEntidad requisicionAnotacionEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            RequisicionService requisicionService0FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad0 = requisicionService0FK.Listar();
            ViewBag.ListaRequisicionEntidad0 = new SelectList(listaRequisicionEntidad0, "IDRequisicion", "NroRequisicion");

            
            return View(requisicionAnotacionEntidad);
        }

        [HttpPost]
        public IActionResult Crear(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            if (ModelState.IsValid)
            {
                RequisicionAnotacionService requisicionAnotacionService = new();

                requisicionAnotacionService.Crear(requisicionAnotacionEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            RequisicionService requisicionService0FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad0 = requisicionService0FK.Listar();
            ViewBag.ListaRequisicionEntidad0 = new SelectList(listaRequisicionEntidad0, "IDRequisicion", "NroRequisicion");


            return View(requisicionAnotacionEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int IDRequisicionAnotacion)
        {
            RequisicionAnotacionEntidad? requisicionAnotacionEntidad = new();
            RequisicionAnotacionService requisicionAnotacionService = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            RequisicionService requisicionService0FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad0 = requisicionService0FK.Listar();
            ViewBag.ListaRequisicionEntidad0 = new SelectList(listaRequisicionEntidad0, "IDRequisicion", "NroRequisicion");


            requisicionAnotacionEntidad = requisicionAnotacionService.BuscarxID(IDRequisicionAnotacion);

            return View(requisicionAnotacionEntidad);
        }

        [HttpPost]
        public IActionResult Modificar(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            if (ModelState.IsValid)
            {
                RequisicionAnotacionService requisicionAnotacionService = new();

                requisicionAnotacionService.Actualizar(requisicionAnotacionEntidad);
                
                return RedirectToAction("Index");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            RequisicionService requisicionService0FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad0 = requisicionService0FK.Listar();
            ViewBag.ListaRequisicionEntidad0 = new SelectList(listaRequisicionEntidad0, "IDRequisicion", "NroRequisicion");


            return View(requisicionAnotacionEntidad);
        }

        [HttpGet]
        public IActionResult Anular(int IDRequisicionAnotacion)
        {
            RequisicionAnotacionEntidad? requisicionAnotacionEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            RequisicionService requisicionService0FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad0 = requisicionService0FK.Listar();
            ViewBag.ListaRequisicionEntidad0 = new SelectList(listaRequisicionEntidad0, "IDRequisicion", "NroRequisicion");


            RequisicionAnotacionService requisicionAnotacionService = new();

            requisicionAnotacionEntidad = requisicionAnotacionService.BuscarxID(IDRequisicionAnotacion);

            return View(requisicionAnotacionEntidad);
        }

        [HttpPost]
        public IActionResult Anular(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            RequisicionAnotacionService requisicionAnotacionService = new();

            requisicionAnotacionService.Eliminar(requisicionAnotacionEntidad);

            /*
            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            RequisicionService requisicionService0FK = new();
            List<RequisicionEntidad>? listaRequisicionEntidad0 = requisicionService0FK.Listar();
            ViewBag.ListaRequisicionEntidad0 = new SelectList(listaRequisicionEntidad0, "IDRequisicion", "NroRequisicion");

            */

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FiltrarRequisicionAnotacion(string filtro)
        {
            RequisicionAnotacionService requisicionAnotacionService = new();
            List<RequisicionAnotacionEntidad>? listaRequisicionAnotacionEntidad;

            listaRequisicionAnotacionEntidad = requisicionAnotacionService.Filtrar(filtro);

            return Json(listaRequisicionAnotacionEntidad.Select(a => new
            {
                _IDRequisicionAnotacion = a.IDRequisicionAnotacion,
                _Anotacion = a.Anotacion
            }));
        }
    }
}
