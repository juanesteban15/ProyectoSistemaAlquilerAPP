using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;

namespace AplicacionAlquiler.implementaciones
{
    public class Conexion : DbContext, IConexion
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Datosgenerales.StringConexion()!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservas>()
                .HasOne(r => r._Vehiculo)
                .WithMany(v => v.Reservas)
                .HasForeignKey(r => r.Vehiculo)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservas>()
                .HasOne(r => r._Cliente)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.Cliente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservas>()
                .HasOne(r => r._EstadoReserva)
                .WithMany(e => e.Reservas)
                .HasForeignKey(r => r.EstadoReserva)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservas>()
                .HasOne(r => r._MetodoPago)
                .WithMany(m => m.Reservas)
                .HasForeignKey(r => r.MetodoPago)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservas>()
                .HasOne(r => r._Tarifa)
                .WithMany(t => t.Reservas)
                .HasForeignKey(r => r.Tarifa)
                .OnDelete(DeleteBehavior.NoAction);
        }





        public DbSet<RegistroAuditoria>? RegistroAuditoria { get; set; }

        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Tarifas>? Tarifas { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Vehiculos>? Vehiculos { get; set; }
        public DbSet<VehiculoFoto>? VehiculoFotos { get; set; }


        public DbSet<Categorias>? Categorias { get; set; }
        public DbSet<ColoresVehiculos>? ColoresVehiculos { get; set; }
        public DbSet<EstadosReservas>? EstadosReservas { get; set; }
        public DbSet<EstadosUsuarios>? EstadosUsuarios { get; set; }
        public DbSet<EstadosVehiculos>? EstadosVehiculos { get; set; }
        public DbSet<Marcas>? Marcas { get; set; }
        public DbSet<MetodosPagos>? MetodosPagos { get; set; }
        public DbSet<SistemasTransmisiones>? SistemasTransmisiones { get; set; }
        public DbSet<TiposCombustibles>? TiposCombustibles { get; set; }
        public DbSet<TiposVehiculos>? TiposVehiculos { get; set; }

    }

}
