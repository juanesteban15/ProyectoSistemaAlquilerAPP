using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Catalogo
{
    [TestClass]
    public class MetodosPagosPruebas
    {
        private IConexion conexion;
        private MetodosPagos? entidad = null;

        public MetodosPagosPruebas()
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
            this.entidad = new MetodosPagos()
            {
                Nombre = "MetodosPagos_Test"
            };
            this.conexion.MetodosPagos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.MetodosPagos!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "MetodosPagos_Test_Mod";

            var entry = this.conexion!.Entry<MetodosPagos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.MetodosPagos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
