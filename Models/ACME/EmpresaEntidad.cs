using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class EmpresaEntidad
    {
        #region Propiedades / Campos mapeados del modelo relacional (BD)
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar Empresa.")]
        [Display(Name = "Código Empresa")]
        public int IDEmpresa { get; set; }

        [Required(ErrorMessage = "Debe seleccionar IDTipoEmpresa.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar IDTipoEmpresa.")]
        [Display(Name = "Tipo Empresa")]
        public int IDTipoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo Empresa es obligatorio.")]
        [Display(Name = "Empresa")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "El campo Direccion es obligatorio.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo RUC es obligatorio.")]
        [Display(Name = "RUC")]
        public string RUC { get; set; }

        [Required(ErrorMessage = "El campo FechaCreacion es obligatorio.")]
        [Display(Name = "FechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "El campo Presupuesto es obligatorio.")]
        [Display(Name = "Presupuesto")]
        public decimal Presupuesto { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio.")]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        #endregion

        #region Propiedades / Campos combinados
        public string? CombEmpresa => $"{RUC} - {Empresa}";
        #endregion

        #region Propiedades / Campos extendidos
        public string? TipoEmpresa { get; set; }
        #endregion

    }
}
