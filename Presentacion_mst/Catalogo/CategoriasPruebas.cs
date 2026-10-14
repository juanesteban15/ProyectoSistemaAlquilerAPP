using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_MST.Catalogo
{
    [TestClass]
    public class CategoriasPruebas
    {
        private IConexion conexion;
        private Categorias? entidad = null;

        public CategoriasPruebas()
        {
            this.conexion = new Conexion();
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

            this.entidad = new Categorias()
            {
                Nombre = "Categorias_Test",
                TipoVehiculo = 2
            };
            this.conexion.Categorias!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Categorias!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Categorias_Test_Mod";

            var entry = this.conexion!.Entry<Categorias>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Categorias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
