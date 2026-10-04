using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Entidades
{
    [TestClass]
    public class TarifasPruebas
    {
        private IConexion conexion;
        private Tarifas? entidad = null;

        public TarifasPruebas()
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
            // Toma el primer vehiculo que exista
            var veh = this.conexion.Vehiculos!.First();

            this.entidad = new Tarifas()
            {
                Vehiculo = 1,
                PrecioPorDia = 200000m,
                FechaInicio = DateTime.Now,
                Activa = true    // inactiva para no mezclarse con la tarifa real del vehiculo
            };
            this.conexion.Tarifas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Tarifas!
                .Include(x => x._Vehiculo)
                .ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            this.entidad!.PrecioPorDia = 250000m;

            var entry = this.conexion!.Entry<Tarifas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Tarifas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}