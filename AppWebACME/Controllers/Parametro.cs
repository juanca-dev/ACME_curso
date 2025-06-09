namespace ACME.Utilidades
{
    public class Parametro
    {
        private readonly IConfiguration _config;

        public Parametro(IConfiguration config)
        {
            _config = config;
        }

        public int ObtenerFilasPorPagina()
        {
            return _config.GetValue<int>("AppSettings:FilasPorPagina");
        }

        public int ObtenerMaxPaginasVisibles()
        {
            return _config.GetValue<int>("AppSettings:MaxPaginasVisibles");
        }

        public int CalcularPaginas(int totalFilas, int filasPorPagina)
        {
            return (int)(Math.Ceiling(totalFilas / (float)filasPorPagina));
        }
    }
}
