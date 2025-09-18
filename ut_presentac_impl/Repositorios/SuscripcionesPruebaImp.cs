using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class SuscripcionesPruebaImp
    {
        private readonly IConexion? iConexion;
        private Suscripciones? entidad;
        private SuscripcionesAplicacion? app;

        public SuscripcionesPruebaImp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new SuscripcionesAplicacion(iConexion);
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
            entidad = new Suscripciones
            {
                IdUsuario = 1, // existe
                IdProveedor = 1, // igual, existe
                MontoMensual = 25000, 
                FechaInicio = DateTime.Today,
                FechaRenovacion = DateTime.Today.AddMonths(1) // se va a renovar 
            };

            app!.Guardar(entidad);
            return entidad.IdSuscripcion > 0;
        }

        public bool Modificar()
        {
            entidad!.MontoMensual = 30000; 
            entidad!.FechaInicio = DateTime.Today;
            entidad!.FechaRenovacion = DateTime.Today.AddMonths(2);

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
           
            entidad!.FechaRenovacion = DateTime.Today.AddDays(-1);
            var entry = iConexion!.Entry<Suscripciones>(entidad);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            iConexion.SaveChanges();

            app!.Borrar(entidad);
            return true;
        }
    }
}

