using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class PresupuestosAplicacion : IPresupuestosAplicacion
    {
        private IConexion? IConexion = null;

        public PresupuestosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Presupuestos? Borrar(Presupuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdPresupuesto == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Categoria = null;



            this.IConexion!.Presupuestos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Presupuestos? Guardar(Presupuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdPresupuesto != 0)
                throw new Exception("lbYaSeGuardo");
 
            //OPERACIONES
            entidad._Usuario = null;
            entidad._Categoria = null;


            this.IConexion!.Presupuestos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Presupuestos? Modificar(Presupuestos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdPresupuesto == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Categoria = null;

            var entry = this.IConexion!.Entry<Presupuestos>(entidad);
            entry.State = EntityState.Modified;

            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Presupuestos> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
