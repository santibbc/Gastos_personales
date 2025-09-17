using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace lib_repositorios.Implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion? IConexion = null;

        public UsuariosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Usuarios? Borrar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdUsuario == 0)
                throw new Exception("lbNoSeGuardo");

            // no se pueden borrar usuarios activos
            if (entidad.Estado)
                throw new Exception("No se puede eliminar un usuario activo.");

            this.IConexion!.Usuarios!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuarios? Guardar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdUsuario != 0)
                throw new Exception("lbYaSeGuardo");

            // el usuario debe tener datos validos
            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("El usuario debe tener nombre y apellido.");

            if (string.IsNullOrWhiteSpace(entidad.Email) || !Regex.IsMatch(entidad.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("El email no es válido.");

            if (string.IsNullOrWhiteSpace(entidad.Contrasena) || entidad.Contrasena.Length < 6)
                throw new Exception("La contraseña debe tener al menos 6 caracteres.");

            // asignar fecha de registro si no se proporcionó
            if (entidad.FechaRegistro == default)
                entidad.FechaRegistro = DateTime.Now;

            // usuarios nuevos siempre activos
            entidad.Estado = true;

            this.IConexion!.Usuarios!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuarios? Modificar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdUsuario == 0)
                throw new Exception("lbNoSeGuardo");

            // usuarios deben tener nombre, apellido y email válidos
            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Apellido))
                throw new Exception("El usuario debe tener nombre y apellido.");

            if (string.IsNullOrWhiteSpace(entidad.Email) || !Regex.IsMatch(entidad.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("El email no es válido.");

            if (!string.IsNullOrWhiteSpace(entidad.Contrasena) && entidad.Contrasena.Length < 6)
                throw new Exception("La contraseña debe tener al menos 6 caracteres.");

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Usuarios> Listar()
        {
            // lsitar solo usuarios activos ordenados por nombre
            return this.IConexion!.Usuarios!
                .Where(u => u.Estado)
                .OrderBy(u => u.Nombre)
                .ToList();
        }
    }
}

