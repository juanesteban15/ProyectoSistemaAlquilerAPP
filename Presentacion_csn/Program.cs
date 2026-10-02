using AplicacionAlquiler.Dominio.Entidades;
using AplicacionAlquiler.implementaciones;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.nucleo;
using Microsoft.EntityFrameworkCore;

try
{
    IConexion conexion = new Conexion();
    conexion.StringConexion = Datosgenerales.ObtenerStringConexion();

    //--ENTIDADES--//

    //cada lista debe llamarse diferente//
    var lista_Usuarios = conexion.Usuarios!.ToList();
    var lista_Vehiculos = conexion.Vehiculos!
    .Include(x => x._Propietario)
    .Include(x => x._ColorVehiculo)
    .Include(x => x._EstadoVehiculo)
    .Include(x => x._TipoVehiculo)
    .Include(x => x._Marca)
    .Include(x => x._SistemaTransmision)
    .Include(x => x._Categoria)
    .Include(x => x._TipoCombustible)
    .ToList();




}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("presentacion_csn");