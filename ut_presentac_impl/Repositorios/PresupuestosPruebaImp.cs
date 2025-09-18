using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class PresupuestosPruebaImp
    {
        private readonly IConexion? iConexion;
        private Presupuestos? entidad;
        private PresupuestosAplicacion? app;

        public PresupuestosPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new PresupuestosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            entidad = new Presupuestos
            {
                IdUsuario = 1,   // con esto se valida en la bd, igual que en el de abajo
                IdCategoria = 1, 
                MontoAsignado = 500000,
                FechaInicio = DateTime.Now.AddDays(-5),
                FechaFin = DateTime.Now.AddDays(30)
            };

            app!.Guardar(entidad);
            return entidad.IdPresupuesto > 0;
        }

        public bool Modificar()
        {
            entidad!.MontoAsignado = 600000; 
            entidad.FechaInicio = DateTime.Now.AddDays(-10);
            entidad.FechaFin = DateTime.Now.AddDays(20);

            app!.Modificar(entidad);
            return true;
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool Borrar()
        {
            // si se quiere borrar la fecha tiene que ser pasada
            entidad!.FechaFin = DateTime.Now.AddDays(-1);

            app!.Modificar(entidad); // se actualiza la fecha
            app!.Borrar(entidad);
            return true;
        }
    }
}

