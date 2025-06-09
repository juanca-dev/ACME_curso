using ACME.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ACME;
using Services.ACME;

namespace ACME.Controllers
{
    public class RequisicionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            RequisicionService requisicionService = new();
            List<RequisicionEntidad>? listaRequisicionEntidad;

            listaRequisicionEntidad = requisicionService.Listar();

            return View(listaRequisicionEntidad);
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

            RequisicionService requisicionService = new();

            ViewBag.nroPagina = nroPagina;
            ViewBag.totalPaginas = parametro.CalcularPaginas(requisicionService.Contar(), tamPagina);
            ViewBag.maxPaginasVisibles = maxPaginasVisibles;

            List<RequisicionEntidad>? listaRequisicionEntidad;

            listaRequisicionEntidad = requisicionService.ObtenerPagina(nroPagina, tamPagina);

            return View(listaRequisicionEntidad);
        }

        [HttpGet]
        public IActionResult BuscarxID(int IDRequisicion)
        {
            RequisicionEntidad? requisicionEntidad;
            RequisicionService? requisicionService = new();

            // Obtener lista de tipos de entidad para la vista (Esto aplica para los FK)
            EmpresaService empresaService0FK = new();
            List<EmpresaEntidad>? listaEmpresaEntidad0 = empresaService0FK.Listar();
            ViewBag.ListaEmpresaEntidad0 = new SelectList(listaEmpresaEntidad0, "IDEmpresa", "Empresa");


            requisicionEntidad = requisicionService.BuscarxID(IDRequisicion);

            // PKs para navegar
            ViewBag.Inicio = requisicionService.IDNavegacion(TipoNavegacion.Inicio, 0);
            ViewBag.Fin = requisicionService.IDNavegacion(TipoNavegacion.Fin, 0);
            ViewBag.Anterior = requisicionService.IDNavegacion(TipoNavegacion.Anterior, IDRequisicion);
            ViewBag.Siguiente = requisicionService.IDNavegacion(TipoNavegacion.Siguiente, IDRequisicion);

            return View(requisicionEntidad);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            RequisicionEntidad requisicionEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            EmpresaService empresaService0FK = new();
            List<EmpresaEntidad>? listaEmpresaEntidad0 = empresaService0FK.Listar();
            ViewBag.ListaEmpresaEntidad0 = new SelectList(listaEmpresaEntidad0, "IDEmpresa", "Empresa");


            return View(requisicionEntidad);
        }

        [HttpPost]
        public IActionResult Crear(RequisicionEntidad requisicionEntidad)
        {
            if (ModelState.IsValid)
            {
                RequisicionService requisicionService = new();

                requisicionService.Crear(requisicionEntidad);

                return RedirectToAction("IndexPaginado");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            EmpresaService empresaService0FK = new();
            List<EmpresaEntidad>? listaEmpresaEntidad0 = empresaService0FK.Listar();
            ViewBag.ListaEmpresaEntidad0 = new SelectList(listaEmpresaEntidad0, "IDEmpresa", "Empresa");


            return View(requisicionEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int IDRequisicion)
        {
            RequisicionEntidad? requisicionEntidad = new();
            RequisicionService requisicionService = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            EmpresaService empresaService0FK = new();
            List<EmpresaEntidad>? listaEmpresaEntidad0 = empresaService0FK.Listar();
            ViewBag.ListaEmpresaEntidad0 = new SelectList(listaEmpresaEntidad0, "IDEmpresa", "Empresa");


            requisicionEntidad = requisicionService.BuscarxID(IDRequisicion);

            return View(requisicionEntidad);
        }

        [HttpPost]
        public IActionResult Modificar(RequisicionEntidad requisicionEntidad)
        {
            if (ModelState.IsValid)
            {
                RequisicionService requisicionService = new();

                requisicionService.Actualizar(requisicionEntidad);

                return RedirectToAction("IndexPaginado");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            EmpresaService empresaService0FK = new();
            List<EmpresaEntidad>? listaEmpresaEntidad0 = empresaService0FK.Listar();
            ViewBag.ListaEmpresaEntidad0 = new SelectList(listaEmpresaEntidad0, "IDEmpresa", "Empresa");


            return View(requisicionEntidad);
        }

        [HttpGet]
        public IActionResult Anular(int IDRequisicion)
        {
            RequisicionEntidad? requisicionEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            EmpresaService empresaService0FK = new();
            List<EmpresaEntidad>? listaEmpresaEntidad0 = empresaService0FK.Listar();
            ViewBag.ListaEmpresaEntidad0 = new SelectList(listaEmpresaEntidad0, "IDEmpresa", "Empresa");


            RequisicionService requisicionService = new();

            requisicionEntidad = requisicionService.BuscarxID(IDRequisicion);

            return View(requisicionEntidad);
        }

        [HttpPost]
        public IActionResult Anular(RequisicionEntidad requisicionEntidad)
        {
            RequisicionService requisicionService = new();

            requisicionService.Eliminar(requisicionEntidad);

            return RedirectToAction("IndexPaginado");
        }

        [HttpGet]
        public IActionResult FiltrarRequisicion(string filtro)
        {
            RequisicionService requisicionService = new();
            List<RequisicionEntidad>? listaRequisicionEntidad;

            listaRequisicionEntidad = requisicionService.Filtrar(filtro);

            return Json(listaRequisicionEntidad.Select(a => new
            {
                _IDRequisicion = a.IDRequisicion,
                _NroRequisicion = a.NroRequisicion
            }));
        }
    }
}
