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

            // no permitir borrar gastos programados en el futuro
            if (entidad.Fecha > DateTime.Now)
                throw new Exception("No se pueden borrar gastos programados en el futuro.");

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

            // validar campos obligatorios
            if (entidad.Monto <= 0)
                throw new Exception("El monto del gasto debe ser mayor a 0.");

            // la descripción es obligatoria si el monto es mayor a 500
            if (entidad.Monto > 500 && string.IsNullOrWhiteSpace(entidad.Descripcion))
                throw new Exception("Debe especificar una descripción para gastos mayores a 500.");

            //la fecha no puede ser en el futuro
            if (entidad.Fecha > DateTime.Now)
                throw new Exception("La fecha del gasto no puede ser en el futuro.");

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

            // no permitir modificar gastos con fecha en el futuro
            if (entidad.Fecha < DateTime.Now.AddDays(-30))
                throw new Exception("No se pueden modificar gastos registrados hace más de 30 días.");

            // monto no puede ser negativo
            if (entidad.Monto < 0)
                throw new Exception("El monto del gasto no puede ser negativo.");

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
            // listar los últimos 100 gastos ordenados por fecha descendente
            return this.IConexion!.Gastos!
                .OrderByDescending(g => g.Fecha)
                .Take(100)
                .ToList();
        }
    }
}

