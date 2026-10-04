using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Catalogo
{
    [TestClass]
    public class ColoresVehiculosPruebas
    {
        private IConexion conexion;
        private ColoresVehiculos? entidad = null;

        public ColoresVehiculosPruebas()
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
            this.entidad = new ColoresVehiculos()
            {
                Nombre = "ColoresVehiculos_Test"
            };
            this.conexion.ColoresVehiculos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.ColoresVehiculos!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "ColoresVehiculos_Test_Mod";

            var entry = this.conexion!.Entry<ColoresVehiculos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.ColoresVehiculos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
