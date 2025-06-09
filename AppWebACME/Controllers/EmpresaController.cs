using ACME.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ACME;
using Services.ACME;

namespace ACME.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            EmpresaService empresaService = new();
            List<EmpresaEntidad>? listaEmpresaEntidad;

            listaEmpresaEntidad = empresaService.Listar();

            return View(listaEmpresaEntidad);
        }

        [HttpGet]
        public IActionResult IndexPaginado(int? pagina)
        {
            // Obtener el objeto configuración de la app
            IConfigurationRoot configuracion = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Parametro parametro = new Parametro(configuracion);
            int tamPagina = parametro.ObtenerFilasPorPagina();
            int maxPaginasVisibles = parametro.ObtenerMaxPaginasVisibles();

            int nroPagina = pagina ?? 1;

            EmpresaService empresaService = new();

            ViewBag.nroPagina = nroPagina;
            ViewBag.totalPaginas = parametro.CalcularPaginas(empresaService.Contar(), tamPagina);
            ViewBag.maxPaginasVisibles = maxPaginasVisibles;

            List<EmpresaEntidad>? listaEmpresaEntidad;

            listaEmpresaEntidad = empresaService.ObtenerPagina(nroPagina, tamPagina);

            return View(listaEmpresaEntidad);
        }

        [HttpGet]
        public IActionResult BuscarxID(int IDEmpresa)
        {
            EmpresaEntidad? empresaEntidad;
            EmpresaService? empresaService = new();

            // Obtener lista de tipos de entidad para la vista (Esto aplica para los FK)
            TipoEmpresaService tipoEmpresaService0FK = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad0 = tipoEmpresaService0FK.Listar();
            ViewBag.ListaTipoEmpresaEntidad0 = new SelectList(listaTipoEmpresaEntidad0, "IDTipoEmpresa", "TipoEmpresa");

            empresaEntidad = empresaService.BuscarxID(IDEmpresa);

            // PKs para navegar
            ViewBag.Inicio = empresaService.IDNavegacion(TipoNavegacion.Inicio, 0);
            ViewBag.Fin = empresaService.IDNavegacion(TipoNavegacion.Fin, 0);
            ViewBag.Anterior = empresaService.IDNavegacion(TipoNavegacion.Anterior, IDEmpresa);
            ViewBag.Siguiente = empresaService.IDNavegacion(TipoNavegacion.Siguiente, IDEmpresa);

            return View(empresaEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int IDEmpresa)
        {
            EmpresaEntidad? empresaEntidad = new();
            EmpresaService empresaService = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            TipoEmpresaService tipoEmpresaService0FK = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad0 = tipoEmpresaService0FK.Listar();
            ViewBag.ListaTipoEmpresaEntidad0 = new SelectList(listaTipoEmpresaEntidad0, "IDTipoEmpresa", "TipoEmpresa");


            empresaEntidad = empresaService.BuscarxID(IDEmpresa);

            return View(empresaEntidad);
        }

        [HttpPost]
        public IActionResult Modificar(EmpresaEntidad empresaEntidad)
        {
            if (ModelState.IsValid)
            {
                EmpresaService empresaService = new();

                empresaService.Actualizar(empresaEntidad);

                return RedirectToAction("IndexPaginado");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            TipoEmpresaService tipoEmpresaService0FK = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad0 = tipoEmpresaService0FK.Listar();
            ViewBag.ListaTipoEmpresaEntidad0 = new SelectList(listaTipoEmpresaEntidad0, "IDTipoEmpresa", "TipoEmpresa");


            return View(empresaEntidad);
        }

        [HttpGet]
        public IActionResult Anular(int IDEmpresa)
        {
            EmpresaEntidad? empresaEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            TipoEmpresaService tipoEmpresaService0FK = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad0 = tipoEmpresaService0FK.Listar();
            ViewBag.ListaTipoEmpresaEntidad0 = new SelectList(listaTipoEmpresaEntidad0, "IDTipoEmpresa", "TipoEmpresa");


            EmpresaService empresaService = new();

            empresaEntidad = empresaService.BuscarxID(IDEmpresa);

            return View(empresaEntidad);
        }

        [HttpPost]
        public IActionResult Anular(EmpresaEntidad empresaEntidad)
        {
            EmpresaService empresaService = new();

            empresaService.Eliminar(empresaEntidad);

            return RedirectToAction("IndexPaginado");
        }

        [HttpGet]
        public IActionResult Crear()
        {
            EmpresaEntidad empresaEntidad = new();

            // Obtener lista de tipos de empresa para la vista (Esto aplica para los FK)
            TipoEmpresaService tipoEmpresaService0FK = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad0 = tipoEmpresaService0FK.Listar();
            ViewBag.ListaTipoEmpresaEntidad0 = new SelectList(listaTipoEmpresaEntidad0, "IDTipoEmpresa", "TipoEmpresa");


            return View(empresaEntidad);
        }

        [HttpPost]
        public IActionResult Crear(EmpresaEntidad empresaEntidad)
        {
            if (ModelState.IsValid)
            {
                EmpresaService empresaService = new();

                empresaService.Crear(empresaEntidad);

                return RedirectToAction("IndexPaginado");
            }

            // Recargar la lista de tipos de empresa si la validación falla (Esto aplica para los FK)
            TipoEmpresaService tipoEmpresaService0FK = new();
            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad0 = tipoEmpresaService0FK.Listar();
            ViewBag.ListaTipoEmpresaEntidad0 = new SelectList(listaTipoEmpresaEntidad0, "IDTipoEmpresa", "TipoEmpresa");


            return View(empresaEntidad);
        }

    }
}
