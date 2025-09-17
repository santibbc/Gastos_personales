using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class MovimientosTarjetaAplicacion : IMovimientosTarjetaAplicacion
    {
        private IConexion? IConexion = null;

        public MovimientosTarjetaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public MovimientosTarjeta? Borrar(MovimientosTarjeta? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMovimiento == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Tarjeta = null;



            this.IConexion!.MovimientosTarjeta!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MovimientosTarjeta? Guardar(MovimientosTarjeta? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdMovimiento != 0)
                throw new Exception("lbYaSeGuardo");

            //OPERACIONES
            entidad._Tarjeta = null;

            this.IConexion!.MovimientosTarjeta!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public MovimientosTarjeta? Modificar(MovimientosTarjeta? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.IdMovimiento == 0)
                throw new Exception("lbNoSeGuardo");

            //OPERACIONES
            entidad._Tarjeta = null;

            var entry = this.IConexion!.Entry<MovimientosTarjeta>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<MovimientosTarjeta> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
