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

            // no se pueden borrar auditorías recientes
            if (entidad.Fecha > DateTime.Now.AddDays(-1))
                throw new Exception("No se pueden borrar registros de auditoría recientes.");


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

            // la operación y la entidad afectada son obligatorias
            if (string.IsNullOrWhiteSpace(entidad.Operacion))
                throw new Exception("Debe especificar la operación de la auditoría.");

            if (string.IsNullOrWhiteSpace(entidad.EntidadAfectada))
                throw new Exception("Debe especificar la entidad afectada en la auditoría.");

            // fecha por defecto si no se proporciona
            if (entidad.Fecha == default)
                entidad.Fecha = DateTime.Now;

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

            // no se podría modificar la entidad afectada ni la operación
            var existente = this.IConexion!.Auditoria!.AsNoTracking()
                .FirstOrDefault(a => a.IdAuditoria == entidad.IdAuditoria);

            if (existente != null && existente.Fecha != entidad.Fecha)
                throw new Exception("No está permitido modificar la fecha de creación de la auditoría.");

            entidad._Usuario = null;

            var entry = this.IConexion!.Entry<Auditoria>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Auditoria> Listar()
        {
            //devolvemos las ultimas 50 auditorias
            return this.IConexion!.Auditoria!
                .OrderByDescending(a => a.Fecha)
                .Take(50)
                .ToList();
        }
    }
}

