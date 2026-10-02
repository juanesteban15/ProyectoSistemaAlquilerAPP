using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.Dominio.Catalogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AplicacionAlquiler.interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }


        //-------Entidades------

        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Tarifas>? Tarifas { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Vehiculos>? Vehiculos { get; set; }


        //-----Catalogo----------//
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

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
