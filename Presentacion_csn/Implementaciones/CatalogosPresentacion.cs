using AplicacionAlquiler.Dominio.Catalogo;
using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.interfaces;
using Presentacion_CSN.Interfaces;

namespace AplicacionAlquiler.interfaces
{
    public class CatalogosPresentacion : ICatalogosPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public CatalogosPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public List<CatalogoItem> Consultar(string tipo)
        {
            return tipo switch
            {
                "TiposVehiculos" => comunicaciones.Ejecutar<List<TiposVehiculos>>("TiposVehiculos/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "ColoresVehiculos" => comunicaciones.Ejecutar<List<ColoresVehiculos>>("ColoresVehiculos/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "EstadosVehiculos" => comunicaciones.Ejecutar<List<EstadosVehiculos>>("EstadosVehiculos/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "EstadosReservas" => comunicaciones.Ejecutar<List<EstadosReservas>>("EstadosReservas/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "EstadosUsuarios" => comunicaciones.Ejecutar<List<EstadosUsuarios>>("EstadosUsuarios/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "Marcas" => comunicaciones.Ejecutar<List<Marcas>>("Marcas/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "Categorias" => comunicaciones.Ejecutar<List<Categorias>>("Categorias/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "TiposCombustibles" => comunicaciones.Ejecutar<List<TiposCombustibles>>("TiposCombustibles/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "SistemasTransmisiones" => comunicaciones.Ejecutar<List<SistemasTransmisiones>>("SistemasTransmisiones/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                "MetodosPagos" => comunicaciones.Ejecutar<List<MetodosPagos>>("MetodosPagos/Consultar", null!).GetAwaiter().GetResult().Select(Mapear).ToList(),
                _ => throw new ArgumentException($"Catálogo desconocido: {tipo}")
            };
        }

        public CatalogoItem Insertar(CatalogoItem entidad) => Guardar(entidad, "Insertar");
        public CatalogoItem Actualizar(CatalogoItem entidad) => Guardar(entidad, "Actualizar");

        public CatalogoItem Borrar(CatalogoItem entidad)
        {
            if (entidad.Id <= 0)
                throw new InvalidOperationException("Selecciona un registro válido para eliminar.");

            return Enviar(entidad, "Borrar");
        }

        private CatalogoItem Guardar(CatalogoItem entidad, string accion)
        {
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new InvalidOperationException("El nombre es obligatorio.");

            if (RequiereTipoVehiculo(entidad.Tipo) && (!entidad.TipoVehiculoId.HasValue || entidad.TipoVehiculoId <= 0))
                throw new InvalidOperationException("Selecciona un tipo de vehículo válido.");

            if (accion == "Insertar" && entidad.Id != 0)
                throw new InvalidOperationException("El registro ya tiene Id.");
            if (accion == "Actualizar" && entidad.Id <= 0)
                throw new InvalidOperationException("El registro no tiene un Id válido.");

            return Enviar(entidad, accion);
        }

        private CatalogoItem Enviar(CatalogoItem item, string accion)
        {
            var entidad = CrearEntidad(item);
            object respuesta = item.Tipo switch
            {
                "TiposVehiculos" => Ejecutar<TiposVehiculos>(item.Tipo, accion, entidad),
                "ColoresVehiculos" => Ejecutar<ColoresVehiculos>(item.Tipo, accion, entidad),
                "EstadosVehiculos" => Ejecutar<EstadosVehiculos>(item.Tipo, accion, entidad),
                "EstadosReservas" => Ejecutar<EstadosReservas>(item.Tipo, accion, entidad),
                "EstadosUsuarios" => Ejecutar<EstadosUsuarios>(item.Tipo, accion, entidad),
                "Marcas" => Ejecutar<Marcas>(item.Tipo, accion, entidad),
                "Categorias" => Ejecutar<Categorias>(item.Tipo, accion, entidad),
                "TiposCombustibles" => Ejecutar<TiposCombustibles>(item.Tipo, accion, entidad),
                "SistemasTransmisiones" => Ejecutar<SistemasTransmisiones>(item.Tipo, accion, entidad),
                "MetodosPagos" => Ejecutar<MetodosPagos>(item.Tipo, accion, entidad),
                _ => throw new ArgumentException($"Catálogo desconocido: {item.Tipo}")
            };

            return Mapear(respuesta);
        }

        private T Ejecutar<T>(string tipo, string accion, object entidad)
        {
            return comunicaciones.Ejecutar<T>($"{tipo}/{accion}", entidad).GetAwaiter().GetResult();
        }

        private static bool RequiereTipoVehiculo(string tipo) =>
            tipo is "Categorias" or "Marcas" or "SistemasTransmisiones" or "TiposCombustibles";

        private static object CrearEntidad(CatalogoItem item) => item.Tipo switch
        {
            "TiposVehiculos" => new TiposVehiculos { Id = item.Id, Nombre = item.Nombre },
            "ColoresVehiculos" => new ColoresVehiculos { Id = item.Id, Nombre = item.Nombre },
            "EstadosVehiculos" => new EstadosVehiculos { Id = item.Id, Nombre = item.Nombre },
            "EstadosReservas" => new EstadosReservas { Id = item.Id, Nombre = item.Nombre },
            "EstadosUsuarios" => new EstadosUsuarios { Id = item.Id, Nombre = item.Nombre },
            "Marcas" => new Marcas { Id = item.Id, Nombre = item.Nombre, TipoVehiculoId = item.TipoVehiculoId!.Value },
            "Categorias" => new Categorias { Id = item.Id, Nombre = item.Nombre, TipoVehiculo = item.TipoVehiculoId!.Value },
            "TiposCombustibles" => new TiposCombustibles { Id = item.Id, Nombre = item.Nombre, TipoVehiculo = item.TipoVehiculoId!.Value },
            "SistemasTransmisiones" => new SistemasTransmisiones { Id = item.Id, Nombre = item.Nombre, TipoVehiculo = item.TipoVehiculoId!.Value },
            "MetodosPagos" => new MetodosPagos { Id = item.Id, Nombre = item.Nombre },
            _ => throw new ArgumentException($"Catálogo desconocido: {item.Tipo}")
        };

        private static CatalogoItem Mapear(TiposVehiculos x) => Nuevo(x.Id, "TiposVehiculos", x.Nombre);
        private static CatalogoItem Mapear(ColoresVehiculos x) => Nuevo(x.Id, "ColoresVehiculos", x.Nombre);
        private static CatalogoItem Mapear(EstadosVehiculos x) => Nuevo(x.Id, "EstadosVehiculos", x.Nombre);
        private static CatalogoItem Mapear(EstadosReservas x) => Nuevo(x.Id, "EstadosReservas", x.Nombre);
        private static CatalogoItem Mapear(EstadosUsuarios x) => Nuevo(x.Id, "EstadosUsuarios", x.Nombre);
        private static CatalogoItem Mapear(Marcas x) => Nuevo(x.Id, "Marcas", x.Nombre, x.TipoVehiculoId);
        private static CatalogoItem Mapear(Categorias x) => Nuevo(x.Id, "Categorias", x.Nombre, x.TipoVehiculo);
        private static CatalogoItem Mapear(TiposCombustibles x) => Nuevo(x.Id, "TiposCombustibles", x.Nombre, x.TipoVehiculo);
        private static CatalogoItem Mapear(SistemasTransmisiones x) => Nuevo(x.Id, "SistemasTransmisiones", x.Nombre, x.TipoVehiculo);
        private static CatalogoItem Mapear(MetodosPagos x) => Nuevo(x.Id, "MetodosPagos", x.Nombre);

        private static CatalogoItem Mapear(object entidad) => entidad switch
        {
            TiposVehiculos x => Mapear(x),
            ColoresVehiculos x => Mapear(x),
            EstadosVehiculos x => Mapear(x),
            EstadosReservas x => Mapear(x),
            EstadosUsuarios x => Mapear(x),
            Marcas x => Mapear(x),
            Categorias x => Mapear(x),
            TiposCombustibles x => Mapear(x),
            SistemasTransmisiones x => Mapear(x),
            MetodosPagos x => Mapear(x),
            _ => throw new ArgumentException("El tipo de catálogo recibido no está soportado.")
        };

        private static CatalogoItem Nuevo(int id, string tipo, string? nombre, int? tipoVehiculoId = null) =>
            new() { Id = id, Tipo = tipo, Nombre = nombre ?? string.Empty, TipoVehiculoId = tipoVehiculoId };
    }
}
