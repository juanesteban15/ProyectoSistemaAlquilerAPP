using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class SistemasTransmisionesPruebas
    {
        private IConexion conexion;
        private SistemasTransmisiones? entidad = null;

        public SistemasTransmisionesPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
        }

        [TestMethod]
        public void Execute()
        {
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
        }

        public void Insertar()
        {

            this.entidad = new SistemasTransmisiones()
            {
                Nombre = "SistemasTransmisiones_Test",
                TipoVehiculo = 1
            };
            this.conexion.SistemasTransmisiones!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.SistemasTransmisiones!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "SistemasTransmisiones_Test_Mod";

            var entry = this.conexion!.Entry<SistemasTransmisiones>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.SistemasTransmisiones!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
