using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class TipoEmpresaEntidad
    {
        #region Propiedades / Campos mapeados del modelo relacional (BD)
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar TipoEmpresa.")]
        [Display(Name = "Código TipoEmpresa")]
        public int IDTipoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo TipoEmpresa es obligatorio.")]
        [Display(Name = "TipoEmpresa")]
        public string TipoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Sigla")]
        public string? Sigla { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio.")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        #endregion

    }
}
