using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class RequisicionAnotacionEntidad
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar RequisicionAnotacion.")]
        [Display(Name = "Código RequisicionAnotacion")]
        public int IDRequisicionAnotacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar IDRequisicion.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar IDRequisicion.")]
        [Display(Name = "IDRequisicion")]
        public int IDRequisicion { get; set; }

        [Required(ErrorMessage = "El campo Anotacion es obligatorio.")]
        [Display(Name = "Anotacion")]
        public string Anotacion { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio.")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        public string? NroRequisicion { get; set; }

    }
}
