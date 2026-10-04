using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Presentacion_mst.Entidades
{
    [TestClass]
    public class ReservasPruebas
    {
        private IConexion conexion;
        private Reservas? entidad = null;

        public ReservasPruebas()
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

            this.entidad = new Reservas()
            {
                Vehiculo = 1,
                Cliente = 2,
                EstadoReserva = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(2),
                MetodoPago = 1,
                Tarifa = 1,
                PrecioTotal = 300000m,
                Observaciones = "Reserva de prueba",
                FechaCreacion = DateTime.Now
            };
            this.conexion.Reservas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Reservas!
                .Include(x => x._Vehiculo)
                .Include(x => x._Cliente)
                .Include(x => x._EstadoReserva)
                .Include(x => x._MetodoPago)
                .Include(x => x._Tarifa)
                .ToList();
            if (!lista.Any(x => x.Id == this.entidad!.Id))
                throw new Exception("No se encontro el registro insertado");
        }

        private void Actualizar()
        {
            var confirmada = this.conexion.EstadosReservas!.First(x => x.Nombre == "Confirmada");

            this.entidad!.EstadoReserva = confirmada.Id;
            this.entidad!.Observaciones = "Reserva confirmada";

            var entry = this.conexion!.Entry<Reservas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Reservas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
