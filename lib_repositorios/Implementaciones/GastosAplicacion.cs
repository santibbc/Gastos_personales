using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class GastosAplicacion : IGastosAplicacion
    {
        private IConexion? IConexion = null;

        public GastosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Gastos? Borrar(Gastos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdGasto == 0)
                throw new Exception("lbNoSeGuardo");
            
            
            //OPERACIONES
            entidad._Usuario = null;
            entidad._Categoria = null;
            entidad._Cuenta = null;




            this.IConexion!.Gastos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Gastos? Guardar(Gastos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdGasto != 0)
                throw new Exception("lbYaSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Categoria = null;
            entidad._Cuenta = null;

            this.IConexion!.Gastos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Gastos? Modificar(Gastos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdGasto == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;
            entidad._Categoria = null;
            entidad._Cuenta = null;

            var entry = this.IConexion!.Entry<Gastos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Gastos> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
