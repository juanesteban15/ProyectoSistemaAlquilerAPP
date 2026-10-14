using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_MST.Entidades
{
    [TestClass]
    public class VehiculosPruebas
    {
        private IConexion conexion;
        private Vehiculos? entidad = null;

        public VehiculosPruebas()
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

            this.entidad = new Vehiculos()
            {
                Placa = "SPH589", 
                Propietario = 4,
                EstadoVehiculo = 2,
                TipoVehiculo = 2,
                Marca = 2,
                ColorVehiculo = 2,
                SistemaTransmision =2,
                Categoria = 5,
                TipoCombustible =3,
                Modelo = 2021,
                FechaRegistro= DateTime.Now
            };
            this.conexion.Vehiculos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Vehiculos!
                .Include(x => x._Propietario)
                .Include(x => x._ColorVehiculo)
                .Include(x => x._EstadoVehiculo)
                .Include(x => x._TipoVehiculo)
                .Include(x => x._Marca)
                .Include(x => x._SistemaTransmision)
                .Include(x => x._Categoria)
                .Include(x => x._TipoCombustible)
                .ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.Modelo = 2025;

            var entry = this.conexion!.Entry<Vehiculos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Vehiculos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
