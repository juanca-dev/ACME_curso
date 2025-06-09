using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class ArticuloEntidad
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar Articulo.")]
        [Display(Name = "Código Articulo")]
        public int IDArticulo { get; set; }

        [Required(ErrorMessage = "Debe seleccionar IDUnidadMedida.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar IDUnidadMedida.")]
        [Display(Name = "IDUnidadMedida")]
        public int IDUnidadMedida { get; set; }

        [Required(ErrorMessage = "El campo Articulo es obligatorio.")]
        [Display(Name = "Articulo")]
        public string Articulo { get; set; }

        [Display(Name = "Precio")]
        public decimal? Precio { get; set; }

        [Display(Name = "StockActual")]
        public decimal? StockActual { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio.")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        public string? UnidadMedida { get; set; }

    }
}
