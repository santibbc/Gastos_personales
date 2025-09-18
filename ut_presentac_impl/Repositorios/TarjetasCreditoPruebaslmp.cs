using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut_presentac_imp.Nucleo;

namespace ut_presentac_imp.Repositorios
{
    [TestClass]
    public class TarjetasCreditoPruebaslmp
    {
        private readonly IConexion? iConexion;
        private TarjetasCredito? entidad;
        private TarjetasCreditoAplicacion? app;

        public TarjetasCreditoPruebaslmp()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new TarjetasCreditoAplicacion(iConexion);
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
            entidad = new TarjetasCredito
            {
                IdUsuario = 1,
                Banco = "Banco Prueba",
                NumeroTarjeta = $"TEST-{Guid.NewGuid().ToString().Substring(0, 8)}",
                Cupo = 255555.0m,
                FechaCorte = DateTime.Now,
                FechaPago = DateTime.Now
            };

            app!.Guardar(entidad);
            return entidad.IdTarjeta > 0;
        }

        public bool Modificar()
        {
            entidad!.Banco = "Banco Modificado 1";
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



