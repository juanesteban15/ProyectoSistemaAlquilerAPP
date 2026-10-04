using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Catalogo
{
    [TestClass]
    public class EstadosReservasPruebas
    {
        private IConexion conexion;
        private EstadosReservas? entidad = null;

        public EstadosReservasPruebas()
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
            this.entidad = new EstadosReservas()
            {
                Nombre = "Denegada"
            };
            this.conexion.EstadosReservas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.EstadosReservas!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "EstadosReservas_Test_Mod";

            var entry = this.conexion!.Entry<EstadosReservas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.EstadosReservas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
