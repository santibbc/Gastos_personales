using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ConfiguracionUsuarioAplicacion : IConfiguracionUsuarioAplicacion
    {
        private IConexion? IConexion = null;

        public ConfiguracionUsuarioAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public ConfiguracionUsuario? Borrar(ConfiguracionUsuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdConfig == 0)
                throw new Exception("lbNoSeGuardo");

            // no se pueden borras las configuraciones importantes
            if (entidad.MonedaPreferida != null && entidad.MonedaPreferida.ToUpper() == "USD")
                throw new Exception("No se puede eliminar la configuración con moneda por defecto USD.");

            entidad._Usuario = null;

            this.IConexion!.ConfiguracionUsuario!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ConfiguracionUsuario? Guardar(ConfiguracionUsuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdConfig != 0)
                throw new Exception("lbYaSeGuardo");

            // moneda preferida es obligatoria
            if (string.IsNullOrWhiteSpace(entidad.MonedaPreferida))
                throw new Exception("Debe definir una moneda preferida.");

            // no pueden haber duplicados por usuario
            bool existe = this.IConexion!.ConfiguracionUsuario!
                .Any(c => c.IdUsuario == entidad.IdUsuario);

            if (existe)
                throw new Exception("Ya existe una configuración para este usuario.");

            entidad._Usuario = null;

            this.IConexion!.ConfiguracionUsuario!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ConfiguracionUsuario? Modificar(ConfiguracionUsuario? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdConfig == 0)
                throw new Exception("lbNoSeGuardo");

            // alerta debe ser mayor que 0 si se define
            if (entidad.LimiteAlertas.HasValue && entidad.LimiteAlertas <= 0)
                throw new Exception("El límite de alertas debe ser mayor que 0.");

            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<ConfiguracionUsuario>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<ConfiguracionUsuario> Listar()
        {
            // devolver ordenado por IdUsuario
            return this.IConexion!.ConfiguracionUsuario!
                .Where(c => c.Notificaciones == true)
                .OrderBy(c => c.IdUsuario)
                .ToList();
        }
    }
}

