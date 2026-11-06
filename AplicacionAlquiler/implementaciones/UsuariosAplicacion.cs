using AplicacionAlquiler.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using AplicacionAlquiler.Dominio.DTOs;
using AplicacionAlquiler.interfaces;
using AplicacionAlquiler.Servicio;
using Microsoft.EntityFrameworkCore;


namespace AplicacionAlquiler.implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private readonly IConexion IConexion;
        private readonly IRegistroAuditoriaAplicacion auditoria;

        private readonly IPasswordHasher<Usuarios> passwordHasher = new PasswordHasher<Usuarios>();



        public UsuariosAplicacion(IConexion conexion, IRegistroAuditoriaAplicacion auditoria)
        {
            this.IConexion = conexion;
            this.auditoria = auditoria;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            // Verifica que el usuario que se desea actualizar exista.
            var usuarioExistente = IConexion.Usuarios!.FirstOrDefault(u => u.Id == entidad.Id);
            if (usuarioExistente == null)
                throw new InvalidOperationException($"No existe un usuario con el Id {entidad.Id}.");

            // Comprueba que la persona asociada exista cuando se indicó una persona.
            if (entidad.Persona.HasValue && !IConexion.Personas!.Any(p => p.Id == entidad.Persona.Value))
                throw new InvalidOperationException($"No existe una persona con el Id {entidad.Persona}.");

            // Evita que el nombre de usuario se repita en otra cuenta.
            if (IConexion.Usuarios!.Any(u => u.NombreUsuario == entidad.NombreUsuario && u.Id != entidad.Id))
                throw new InvalidOperationException($"Ya existe el nombre de usuario {entidad.NombreUsuario}.");

            usuarioExistente.Persona = entidad.Persona;
            usuarioExistente.NombreUsuario = entidad.NombreUsuario;
            usuarioExistente.Contrasena = entidad.Contrasena;
            usuarioExistente.Rol = entidad.Rol;
            usuarioExistente.Estadousuario = entidad.Estadousuario;
            usuarioExistente.FechaEliminacion = entidad.FechaEliminacion;
            // El contexto usa consultas sin seguimiento; se marca explícitamente la entidad como modificada.
            IConexion.Entry(usuarioExistente).State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "UPDATE", Detalle = $"Se actualizó el usuario con Id {entidad.Id}" });
            return usuarioExistente;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            // Recupera el usuario y confirma que exista antes de eliminarlo.
            var usuarioExistente = IConexion.Usuarios!.FirstOrDefault(u => u.Id == entidad.Id);
            if (usuarioExistente == null)
                throw new InvalidOperationException($"No existe un usuario con el Id {entidad.Id}.");

            // No permite eliminar al usuario si tiene vehículos o reservas relacionados.
            if (IConexion.Vehiculos!.Any(v => v.Propietario == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el usuario con Id {entidad.Id} porque es propietario de vehículos.");
            if (IConexion.Reservas!.Any(r => r.Cliente == entidad.Id))
                throw new InvalidOperationException($"No se puede eliminar el usuario con Id {entidad.Id} porque tiene reservas asociadas.");

            this.IConexion.Usuarios!.Remove(usuarioExistente);
            this.IConexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "DELETE", Detalle = $"Se eliminó el usuario con Id {entidad.Id}" });
            return usuarioExistente;
        }

        public List<Usuarios> Consultar()
        {
            var usuarios = this.IConexion.Usuarios!
                .Include(x => x._Persona).Include(x => x._EstadoUsuario)
                .ToList();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "CONSULTA", Detalle = $"Se consultaron los usuarios. Cantidad: {usuarios.Count}" });
            return usuarios;
        }


        public List<Usuarios> Buscar(Usuarios entidad)
        {
            return this.IConexion.Usuarios!
                .Where(x => x.NombreUsuario!.Contains(entidad.NombreUsuario!))
                .ToList();
        }



        public Usuarios Insertar(Usuarios entidad)
        {
            var ExistePersona = this.IConexion.Personas!
                .FirstOrDefault(h => h.Id == entidad.Persona);

            if (ExistePersona == null)
            {
                throw new InvalidOperationException(
                    $"No existe una persona con el Id {entidad.Persona}.");
            }

            // Evita crear otra cuenta con el mismo nombre de usuario.
            if (this.IConexion.Usuarios!.Any(u => u.NombreUsuario == entidad.NombreUsuario))
            {
                throw new InvalidOperationException($"Ya existe el nombre de usuario {entidad.NombreUsuario}.");
            }

            if (string.IsNullOrWhiteSpace(entidad.Contrasena))
            {
                throw new InvalidOperationException("La contraseña es obligatoria.");
            }

            entidad.Contrasena = passwordHasher.HashPassword(
                entidad,
                entidad.Contrasena);


            this.IConexion.Usuarios!.Add(entidad);
            this.IConexion.SaveChanges();
            auditoria.Insertar(new RegistroAuditoria { FechaUtc = DateTime.UtcNow, Movimiento = "INSERT", Detalle = $"Se insertó el usuario con Id {entidad.Id}" });

            return entidad;
        }

        public LoginResponse IniciarSesion(LoginRequest solicitud)
        {
            var identificador = solicitud.Identificador.Trim();

            var usuario = IConexion.Usuarios!
                .Include(u => u._Persona)
                .FirstOrDefault(u =>
                    u.NombreUsuario == identificador ||
                    (u._Persona != null && u._Persona.Email == identificador));

            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                return new LoginResponse { Autenticado = false };
            }

            var resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.Contrasena,
                solicitud.Contrasena);

            if (resultado == PasswordVerificationResult.Failed)
            {
                return new LoginResponse { Autenticado = false };
            }

            return new LoginResponse
            {
                Autenticado = true,
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol
            };
        }




        public bool Registrar(RegistroRequest solicitud)
        {
            if (string.IsNullOrWhiteSpace(solicitud.NombreUsuario) ||
                string.IsNullOrWhiteSpace(solicitud.Contrasena) ||
                string.IsNullOrWhiteSpace(solicitud.Email))
            {
                throw new InvalidOperationException(
                    "El correo, el nombre de usuario y la contraseña son obligatorios.");
            }

            if (IConexion.Usuarios!.Any(u => u.NombreUsuario == solicitud.NombreUsuario))
            {
                throw new InvalidOperationException("Ese nombre de usuario ya está registrado.");
            }

            if (IConexion.Personas!.Any(p => p.Email == solicitud.Email))
            {
                throw new InvalidOperationException("Ese correo ya está registrado.");
            }

            if (IConexion.Personas!.Any(p => p.Telefono == solicitud.Telefono))
            {
                throw new InvalidOperationException("Ese teléfono ya está registrado.");
            }
            if (!solicitud.FechaNacimiento.HasValue)
            {
                throw new InvalidOperationException("La fecha de nacimiento es obligatoria.");
            }

            if (solicitud.FechaNacimiento.Value.Date > DateTime.Today)
            {
                throw new InvalidOperationException(
                    "La fecha de nacimiento no es valida.");
            }


            var nombreUsuario = solicitud.NombreUsuario.Trim();
            var email = solicitud.Email.Trim();

            if (IConexion.Usuarios!.Any(u => u.NombreUsuario == nombreUsuario))
            {
                throw new InvalidOperationException(
                    "Ese nombre de usuario ya está registrado.");
            }

            if (IConexion.Personas!.Any(p => p.Email == email))
            {
                throw new InvalidOperationException(
                    "Ese correo electrónico ya está registrado.");
            }

            if (!string.IsNullOrWhiteSpace(solicitud.NumeroDocumento) &&
                IConexion.Personas.Any(p =>
                    p.NumeroDocumento == solicitud.NumeroDocumento.Trim()))
            {
                throw new InvalidOperationException(
                    "Ese número de documento ya está registrado.");
            }


            var persona = new Personas
            {
                Nombre = solicitud.Nombre,
                Apellido = solicitud.Apellido,
                Email = solicitud.Email,
                PaisNacimiento = solicitud.PaisNacimiento,
                FechaNacimiento = solicitud.FechaNacimiento,
                TipoDocumento = solicitud.TipoDocumento,
                NumeroDocumento = solicitud.NumeroDocumento,
                Genero = solicitud.Genero,
                ComplementoPais = solicitud.ComplementoPais,
                Telefono = solicitud.Telefono
            };

            var usuario = new Usuarios
            {
                _Persona = persona,
                NombreUsuario = solicitud.NombreUsuario,
                Rol = "Cliente",
                Estadousuario = 2,
                FechaCreacion = DateTime.UtcNow
            };

            usuario.Contrasena = passwordHasher.HashPassword(
                usuario,
                solicitud.Contrasena);






            IConexion.Usuarios.Add(usuario);
            IConexion.SaveChanges();

            auditoria.Insertar(new RegistroAuditoria
            {
                FechaUtc = DateTime.UtcNow,
                Movimiento = "INSERT",
                Detalle = $"Se registró el usuario con Id {usuario.Id}"
            });

            return true;
        }






    }
}
