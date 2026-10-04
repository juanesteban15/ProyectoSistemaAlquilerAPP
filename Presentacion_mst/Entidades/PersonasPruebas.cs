using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Entidades
{
    [TestClass]
    public class PersonasPruebas
    {
        private IConexion conexion;
        private Personas? entidad = null;

        public PersonasPruebas()
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
            this.entidad = new Personas()
            {
                Nombre = "Sebas",
                Apellido = "daza",
                Email = "sebas.test@correo.com",
                PaisNacimiento = "Colombia",
                FechaNacimiento = new DateTime(1995, 5, 20),
                TipoDocumento = "CC",
                NumeroDocumento = "9999999999",
                Genero = "Hombre",
                ComplementoPais = "+57",
                Telefono = "3001234567"
            };
            this.conexion.Personas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Personas!.ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Telefono = "3109999999";

            var entry = this.conexion!.Entry<Personas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Personas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
