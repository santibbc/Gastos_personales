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

            // no se pueden borrar proveedores con deudas o servicios activos
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("No se puede eliminar un proveedor sin nombre.");

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

            // nombre y categoría son obligatorios
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("El proveedor debe tener un nombre.");

            if (string.IsNullOrWhiteSpace(entidad.Categoria))
                throw new Exception("El proveedor debe tener una categoría.");

            if (!string.IsNullOrWhiteSpace(entidad.Contacto) && entidad.Contacto.Length < 5)
                throw new Exception("El contacto del proveedor debe tener al menos 5 caracteres.");

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

            // propiedades obligatorias
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("El proveedor debe tener un nombre válido.");

            if (string.IsNullOrWhiteSpace(entidad.Categoria))
                throw new Exception("El proveedor debe tener una categoría válida.");

            var entry = this.IConexion!.Entry<ProveedoresServicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<ProveedoresServicios> Listar()
        {
            // nombrar los proveedores en orden alfabético
            return this.IConexion!.ProveedoresServicios!
                .OrderBy(p => p.Nombre)
                .ToList();
        }
    }
}

