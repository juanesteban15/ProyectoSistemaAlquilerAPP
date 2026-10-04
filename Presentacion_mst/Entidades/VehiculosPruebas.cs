using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Entidades
{
    [TestClass]
    public class VehiculosPruebas
    {
        private IConexion conexion;
        private Vehiculos? entidad = null;

        public VehiculosPruebas()
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

            this.entidad = new Vehiculos()
            {
                Placa = "SPH589", 
                Propietario = 1,
                EstadoVehiculo = 1,
                TipoVehiculo = 2,
                Marca = 1,
                ColorVehiculo = 1,
                SistemaTransmision =1,
                Categoria = 1,
                TipoCombustible =1,
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
