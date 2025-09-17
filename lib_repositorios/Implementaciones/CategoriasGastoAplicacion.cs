
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

            // no se puede borrar la categoría base "Otros"
            if (entidad.Nombre != null && entidad.Nombre.ToLower() == "otros")
                throw new Exception("No se puede eliminar la categoría base 'Otros'.");

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

            // el nombre es obligatorio
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("El nombre de la categoría es obligatorio.");

            // no pueden haber duplicaaados
            bool existe = this.IConexion!.CategoriasGasto!
                .Any(c => c.Nombre!.ToLower() == entidad.Nombre!.ToLower());

            if (existe)
                throw new Exception($"Ya existe una categoría con el nombre '{entidad.Nombre}'.");

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

            // el nombre es obligatorio
            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("El nombre de la categoría es obligatorio.");

            var entry = this.IConexion!.Entry<CategoriasGasto>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<CategoriasGasto> Listar()
        {
            // listar categorias activas
            return this.IConexion!.CategoriasGasto!
                .OrderBy(c => c.Nombre)
                .ToList();
        }
    }
}
