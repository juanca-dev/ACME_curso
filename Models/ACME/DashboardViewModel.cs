using Models.ACME;
using System;
using System.Collections.Generic;

namespace AppWebACME.Models
{
    public class DashboardViewModel
    {
        public int TotalRequisiciones { get; set; }
        public int TotalAprobadas { get; set; }
        public int TotalEmpresasActivas { get; set; }
        public int TotalArticulos { get; set; }

        public List<RequisicionEntidad> RequisicionesRecientes { get; set; } = new List<RequisicionEntidad>();
        public Dictionary<string, int> EmpresasPorTipo { get; set; } = new Dictionary<string, int>();
    }
}