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
 
            //OPERACIONES
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

            //OPERACIONES
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

            //OPERACIONES
            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<ConfiguracionUsuario>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<ConfiguracionUsuario> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
