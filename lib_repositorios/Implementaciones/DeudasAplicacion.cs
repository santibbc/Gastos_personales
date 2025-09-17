using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class DeudasAplicacion : IDeudasAplicacion
    {
        private IConexion? IConexion = null;

        public DeudasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Deudas? Borrar(Deudas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdDeuda == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;



            this.IConexion!.Deudas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Deudas? Guardar(Deudas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdDeuda != 0)
                throw new Exception("lbYaSeGuardo");

            //OPERACIONES
            entidad._Usuario = null;

            this.IConexion!.Deudas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Deudas? Modificar(Deudas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdDeuda == 0)
                throw new Exception("lbNoSeGuardo");
  
            
            //OPERACIONES
            entidad._Usuario = null;


            var entry = this.IConexion!.Entry<Deudas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Deudas> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
