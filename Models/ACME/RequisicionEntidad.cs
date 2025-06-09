using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class RequisicionEntidad
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar Requisicion.")]
        [Display(Name = "Código Requisicion")]
        public int IDRequisicion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar IDEmpresa.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar IDEmpresa.")]
        [Display(Name = "IDEmpresa")]
        public int IDEmpresa { get; set; }

        [Required(ErrorMessage = "El campo NroRequisicion es obligatorio.")]
        [Display(Name = "NroRequisicion")]
        public string NroRequisicion { get; set; }

        [Required(ErrorMessage = "El campo FechaEmision es obligatorio.")]
        [Display(Name = "FechaEmision")]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "El campo Aprobada es obligatorio.")]
        [Display(Name = "Aprobada")]
        public bool Aprobada { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio.")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        public string? RUC { get; set; }

        public string? Empresa { get; set; }

        #region Referencia a tablas relacionadas
        public List<RequisicionDetalleEntidad>? ListaRequisicionDetalle { get; set; } = new List<RequisicionDetalleEntidad>();
        public List<RequisicionAnotacionEntidad>? ListaRequisicionAnotacion { get; set; } = new List<RequisicionAnotacionEntidad>();
        #endregion
    }
}
