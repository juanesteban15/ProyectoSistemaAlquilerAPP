using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Catalogo
{
    [TestClass]
    public class EstadosVehiculosPruebas
    {
        private IConexion conexion;
        private EstadosVehiculos? entidad = null;

        public EstadosVehiculosPruebas()
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
            this.entidad = new EstadosVehiculos()
            {
                Nombre = "EstadosVehiculos_Test"
            };
            this.conexion.EstadosVehiculos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.EstadosVehiculos!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "EstadosVehiculos_Test_Mod";

            var entry = this.conexion!.Entry<EstadosVehiculos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.EstadosVehiculos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
