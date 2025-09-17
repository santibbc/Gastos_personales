using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class ProveedoresServiciosAplicacion : IProveedoresServiciosAplicacion
    {
        private IConexion? IConexion = null;

        public ProveedoresServiciosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public ProveedoresServicios? Borrar(ProveedoresServicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdProveedor == 0)
                throw new Exception("lbNoSeGuardo");



            this.IConexion!.ProveedoresServicios!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ProveedoresServicios? Guardar(ProveedoresServicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdProveedor != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.ProveedoresServicios!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ProveedoresServicios? Modificar(ProveedoresServicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdProveedor == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<ProveedoresServicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<ProveedoresServicios> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
