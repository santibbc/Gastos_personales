using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class SuscripcionesPruebaslmp
    {
        private readonly IConexion? iConexion;
        private Suscripciones? entidad;
        private SuscripcionesAplicacion? app;

        public SuscripcionesPruebaslmp()
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
                IdSuscripcion = 1,
                Banco = "Banco Prueba",
                NumeroCuenta = $"TEST-{Guid.NewGuid().ToString().Substring(0, 8)}",
                TipoCuenta = "Ahorros",
                SaldoInicial = 0,
                Moneda = "COP"
            };

            app!.Guardar(entidad);
            return entidad.IdSuscripcion > 0;
        }

        public bool Modificar()
        {
            entidad!.Banco = "Banco Modificado";
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

            app!.Borrar(entidad);
            return true;
        }
    }
}



