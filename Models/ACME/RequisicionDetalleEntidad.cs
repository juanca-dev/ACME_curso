using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class RequisicionDetalleEntidad
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar RequisicionDetalle.")]
        [Display(Name = "Código RequisicionDetalle")]
        public int IDRequisicionDetalle { get; set; }

        [Required(ErrorMessage = "Debe seleccionar IDRequisicion.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar IDRequisicion.")]
        [Display(Name = "IDRequisicion")]
        public int IDRequisicion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar IDArticulo.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar IDArticulo.")]
        [Display(Name = "IDArticulo")]
        public int IDArticulo { get; set; }

        [Required(ErrorMessage = "El campo Linea es obligatorio.")]
        [Display(Name = "Linea")]
        public short Linea { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio.")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        public string? Articulo { get; set; }
        public string? NroRequisicion { get; set; }


    }
}
