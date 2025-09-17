using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class AuditoriaAplicacion : IAuditoriaAplicacion
    {
        private IConexion? IConexion = null;

        public AuditoriaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Auditoria? Borrar(Auditoria? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdAuditoria == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            

            this.IConexion!.Auditoria!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Auditoria? Guardar(Auditoria? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdAuditoria != 0)
                throw new Exception("lbYaSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;

            this.IConexion!.Auditoria!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Auditoria? Modificar(Auditoria? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdAuditoria == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<Auditoria>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Auditoria> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
