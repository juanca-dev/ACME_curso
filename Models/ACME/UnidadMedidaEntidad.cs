using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class UnidadMedidaEntidad
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar UnidadMedida.")]
        [Display(Name = "Código UnidadMedida")]
        public int IDUnidadMedida { get; set; }

        [Required(ErrorMessage = "El campo UnidadMedida es obligatorio.")]
        [Display(Name = "UnidadMedida")]
        public string UnidadMedida { get; set; }

        [Required(ErrorMessage = "El campo Sigla es obligatorio.")]
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio.")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

    }
}
