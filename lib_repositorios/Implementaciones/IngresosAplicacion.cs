using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class IngresosAplicacion : IIngresosAplicacion
    {
        private IConexion? IConexion = null;

        public IngresosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Ingresos? Borrar(Ingresos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdIngreso == 0)
                throw new Exception("lbNoSeGuardo");
 
            //OPERACIONES
            entidad._Usuario = null;
            entidad._Cuenta = null;



            this.IConexion!.Ingresos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Ingresos? Guardar(Ingresos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdIngreso != 0)
                throw new Exception("lbYaSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Cuenta = null;

            this.IConexion!.Ingresos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Ingresos? Modificar(Ingresos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdIngreso == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Cuenta = null;

            var entry = this.IConexion!.Entry<Ingresos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Ingresos> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
