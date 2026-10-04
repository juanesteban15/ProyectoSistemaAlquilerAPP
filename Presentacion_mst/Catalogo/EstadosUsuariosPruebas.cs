using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Catalogo
{
    [TestClass]
    public class EstadosUsuariosPruebas
    {
        private IConexion conexion;
        private EstadosUsuarios? entidad = null;

        public EstadosUsuariosPruebas()
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
            this.entidad = new EstadosUsuarios()
            {
                Nombre = "Activo",
            };
            this.conexion.EstadosUsuarios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.EstadosUsuarios!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }


        private void Actualizar()
        {
            this.entidad!.Nombre = "Inactivo";

            var entry = this.conexion!.Entry<EstadosUsuarios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.EstadosUsuarios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}