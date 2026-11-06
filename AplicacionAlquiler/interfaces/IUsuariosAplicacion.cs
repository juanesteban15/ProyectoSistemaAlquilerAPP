using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.Dominio.Entidades;

namespace AplicacionAlquiler.interfaces
{
    public interface IUsuariosAplicacion
    {
        Usuarios Insertar(Usuarios entidad);
        List<Usuarios> Consultar();
        List<Usuarios> Buscar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);
        LoginResponse IniciarSesion(LoginRequest solicitud);
        bool Registrar(RegistroRequest solicitud);
    }
}
