
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class CategoriasGastoAplicacion : ICategoriasGastoAplicacion
    {
        private IConexion? IConexion = null;

        public CategoriasGastoAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public CategoriasGasto? Borrar(CategoriasGasto? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdCategoria == 0)
                throw new Exception("lbNoSeGuardo");



            this.IConexion!.CategoriasGasto!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public CategoriasGasto? Guardar(CategoriasGasto? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdCategoria != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.CategoriasGasto!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public CategoriasGasto? Modificar(CategoriasGasto? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdCategoria == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<CategoriasGasto>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<CategoriasGasto> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
