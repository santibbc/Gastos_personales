using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class MetasAhorroAplicacion : IMetasAhorroAplicacion
    {
        private IConexion? IConexion = null;

        public MetasAhorroAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public MetasAhorro? Borrar(MetasAhorro? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMeta == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;


            this.IConexion!.MetasAhorro!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MetasAhorro? Guardar(MetasAhorro? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdMeta != 0)
                throw new Exception("lbYaSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;

            this.IConexion!.MetasAhorro!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MetasAhorro? Modificar(MetasAhorro? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMeta == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<MetasAhorro>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<MetasAhorro> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
