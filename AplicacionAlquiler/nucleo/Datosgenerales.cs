namespace AplicacionAlquiler.nucleo
{
    public class Datosgenerales
    {
        public static string ObtenerStringConexion()
        {
            return "server=(localdb)\\MSSQLLocalDB;database=proyecto_db;Integrated Security=True;TrustServerCertificate=true;";
        }
    }
}
